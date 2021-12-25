<template>
    <div class="form-group row">
    <label class="col-4 col-form-label" for="Title">Title</label> 
    <div class="col-8">
      <input id="title" name="title" placeholder="title" type="text" class="form-control" v-model="title">
    </div>
  </div>
  <div class="form-group row">
    <label for="" class="col-4 col-form-label">TicketStatus</label> 
    <div class="col-8">
      <select id="ticketStatus" name="ticketStatus" v-model="ticketStatus" class="custom-select" >
        <option v-for="ticketStatus in ticketStatuses" :key="ticketStatus.value" :value="ticketStatus.value">
            {{ ticketStatus.name }}
        </option>
      </select>
    </div>
  </div>
  <div class="form-group row">
    <label for="Summary" class="col-4 col-form-label">Summary</label> 
    <div class="col-8">
      <textarea id="summary" name="summary" type="text" class="form-control" v-model="summary"></textarea>
    </div>
  </div>
  <div class="form-group row">
    <label for="" class="col-4 col-form-label">Description</label> 
    <div class="col-8">
      <textarea id="description" name="description" placeholder="description" type="text" class="form-control" v-model="description"></textarea>
    </div>
  </div>
    <div class="form-group row">
    <label for="Severity" class="col-4 col-form-label">Severity</label> 
    <div class="col-8">
      <input type="number" id="severity" name="severity" placeholder="Severity" class="form-control" v-model="severity" />
    </div>
  </div>
  <div class="form-group row">
    <label for="Priority" class="col-4 col-form-label">Priority</label> 
    <div class="col-8">
      <input type="number" id="priority" name="priority" placeholder="Priority" class="form-control" v-model="priority" />
    </div>
  </div>  
  <div class="form-group row">
    <div class="offset-4 col-8">
      <button @click="update" name="submit" type="button" class="btn btn-primary mr-3">Submit</button>
      <router-link to="/Ticket">
            <button name="cancel" type="button" class="btn btn-danger">Cancel</button>
        </router-link>
    </div>
  </div>
</template>

<script>
import dataService from '../../services/dataService';
export default {
    data() {
        return {
            title: '',
            ticketStatus: 0,
            summary: '',
            description: '',
            severity:'',
            priority:'',
            ticketStatuses:[],
        }
    },
    methods: {
        getTicket() {
            var id = this.$route.params.id;
            dataService.getTicket(id).then(
                result => {
                if(result.status === 200 && result.data.code === 0) {
                    this.title = result.data.data.title;
                    this.ticketStatus = result.data.data.ticketStatus;
                    this.summary = result.data.data.summary;
                    this.description = result.data.data.description;
                    this.priority = result.data.data.priority;
                    this.severity = result.data.data.severity;
                }
            })
        },
        update() {
            var id = this.$route.params.id;
            dataService.updateTicket(id, {
                title: this.title,
                ticketStatus: this.ticketStatus,
                summary: this.summary,
                description: this.description,
                priority: this.priority,
                severity: this.severity
            }).then(result => {
                if(result.status === 200 && result.data.code === 0) {
                    alert('Success');
                    this.$router.push('/Ticket');
                }
            });
        },
        getStatus() {
          dataService.getTicketStatus().then(result => {
            this.ticketStatuses = result.data.data;
          })
        },
        getTicketType(){
          dataService.getTicketType().then(result => {
            this.ticketTypes = result.data.data;
          })
        }
    },
    mounted() {
        this.getTicket();
        this.getStatus();
        this.getTicketType();
    }
}
</script>

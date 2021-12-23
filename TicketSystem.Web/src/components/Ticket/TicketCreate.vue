<template>
    <div class="form-group row">
    <label class="col-4 col-form-label" for="Title">Title</label> 
    <div class="col-8">
      <input id="title" name="title" placeholder="title" type="text" class="form-control" v-model="title">
    </div>
  </div>
  <div class="form-group row">
    <label for="" class="col-4 col-form-label">TicketType</label> 
    <div class="col-8">
      <select id="ticketType" name="ticketType" v-model="ticketType" class="custom-select" >
        <option v-for="ticketType in ticketTypes" :key="ticketType.value" :value="ticketType.value">
            {{ ticketType.name }}
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
    <div class="offset-4 col-4 ">
        <button @click="create" name="submit" type="button" class="btn btn-primary mr-3">Submit</button>
        <router-link to="/Ticket">
            <button name="cancel" type="button" class="btn btn-danger">Cancel</button>
        </router-link>
    </div>
  </div>
</template>

<script>
import dataService from '../../services/dataService'
export default {
    data() {
        return {
            title: '',
            ticketType: 0,
            summary: '',
            description: '',
            ticketTypes:[],
        }
    },
    methods: {
        create() {
            dataService.createTicket({
                title: this.title,
                ticketType: this.ticketType,
                summary: this.summary,
                description: this.description
            }).then(dataService.handlerSuccess).catch(dataService.handleError);
        }
    },
    mounted() {
        this.ticketTypes = [{name:'type',value:1},{name:'type2',value:2}]
    }
}
</script>

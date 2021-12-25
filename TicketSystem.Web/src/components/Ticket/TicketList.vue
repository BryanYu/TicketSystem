<template>
    <p v-if="!tickets"><em>No Data Found</em></p>
    <router-link to="/Ticket/Create" v-if="canCreate">Create</router-link>
<div v-if="tickets">
<table class="table">
  <thead>
    <tr>
        <th scope="col">Title</th>
        <th scope="col">TicketType</th>
        <th scope="col">TicketStatus</th>
        <th scope="col">Summary</th>
        <th scope="col">Description</th>
        <th scope="col">Severity</th>
        <th scope="col">Priority</th>
        <th scope="col">CreateBy</th>
        <th scope="col">UpdateBy</th>
        <th scope="col">CreateDate</th>
        <th scope="col">UpdateDate</th>
        <th scope="col"></th>
        <th scope="col"></th>
        <th scope="col"></th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="ticket of tickets" v-bind:key="ticket.id">
                <th scope="row">{{ ticket.title }}</th>
                <td>{{ ticket.ticketTypeName }}</td>
                <td>{{ ticket.ticketStatusName }}</td>
                <td>{{ ticket.summary }}</td>
                <td>{{ ticket.description }}</td>
                <td>{{ ticket.severity }}</td>
                <td>{{ ticket.priority }}</td>
                <td>{{ ticket.createBy }}</td>
                <td>{{ ticket.updateBy }}</td>
                <td>{{ ticket.createDate }}</td>
                <td>{{ ticket.updateDate }}</td>
                <td>
                    <button @click="resolveTicket(ticket.id)" type="button" class="btn btn-success btn-user btn-block" v-if="ticket.canResolve">Resolve</button>
                </td>
                <td>
                    <router-link :to="{
                        name: 'TicketEdit',
                        params: {
                            id: ticket.id
                        }
                    }" v-if="ticket.canEdit">
                        <button type="button" class="btn btn-info btn-user btn-block">Edit</button>
                    </router-link>
                </td>
                <td>
                    <button @click="deleteTicket(ticket.id)" type="button" class="btn btn-danger btn-user btn-block" v-if="ticket.canDelete">Delete</button>
                </td>
            </tr>
  </tbody>
</table>
</div>
</template>
<script>
import constant from '../../common/constant';
import dataService from '../../services/dataService';
    export default {
        data() {
            return {
                tickets: [],
                canCreate: true,
                canEdit: true,
                canDelete: true,
                canResolve: true,
                ticketTypes:[]
            }
        },
        methods: {
            getTickets() {
                dataService.getTickets()
                .then(result => 
                {
                    this.tickets = result.data.data;
                    if(this.tickets == null) {
                        return;
                    }
                    this.tickets.forEach(item => {
                        if(this.canResolve && item.ticketStatus !== constant.permissions.resolve){
                            item.canResolve = true;
                        }
                        if(this.canEdit && this.ticketTypes.includes(item.ticketType)) {
                            item.canEdit = true;
                        }
                        if(this.canDelete && this.ticketTypes.includes(item.ticketType)) {
                            item.canDelete = true;
                        }
                    })
                });
            },
            deleteTicket(id) {
                if(confirm('Sure?')){
                    dataService.deleteTicket(id).
                    then(result => {
                        if(result.status === 200 && result.data.code === 0) {
                            this.getTickets();
                        }
                    });
                }
            },
            resolveTicket(id) {
                if(confirm('Sure?')) {
                    dataService.resolveTicket(id).then(result => {
                        if(result.status === 200 && result.data.code === 0) {
                            this.getTickets();
                        }
                    });
                }
            }
        },
        mounted() {
            var permission = sessionStorage.getItem(constant.sesstionStorageKey.permissions);
            var ticketTypes = sessionStorage.getItem(constant.sesstionStorageKey.ticketTypes);
            this.canCreate = permission.includes(constant.permissions.create);
            this.canEdit = permission.includes(constant.permissions.edit);
            this.canDelete = permission.includes(constant.permissions.delete);
            this.canResolve = permission.includes(constant.permissions.resolve);
            this.ticketTypes = ticketTypes


            this.getTickets();
        }
    }
</script>
<template>
    <p v-if="!tickets"><em>No Data Found</em></p>
    <router-link to="/Ticket/Create">Create</router-link>
<div v-if="tickets">
<table class="table">
  <thead>
    <tr>
        <th scope="col">Title</th>
        <th scope="col">TicketType</th>
        <th scope="col">TicketStatus</th>
        <th scope="col">Summary</th>
        <th scope="col">Description</th>
        <th scope="col">CreateBy</th>
        <th scope="col">UpdateBy</th>
        <th scope="col">CreateDate</th>
        <th scope="col">UpdateDate</th>
        <th scope="col"></th>
        <th scope="col"></th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="ticket of tickets" v-bind:key="ticket.id">
                <th scope="row">{{ ticket.title }}</th>
                <td>{{ ticket.ticketType }}</td>
                <td>{{ ticket.ticketStatus }}</td>
                <td>{{ ticket.summary }}</td>
                <td>{{ ticket.description }}</td>
                <td>{{ ticket.createBy }}</td>
                <td>{{ ticket.updateBy }}</td>
                <td>{{ ticket.createDate }}</td>
                <td>{{ ticket.updateDate }}</td>
                <td>
                    <router-link :to="{
                        name: 'TicketEdit',
                        params: {
                            id: ticket.id
                        }
                    }">
                        <button type="button" class="btn btn-info btn-user btn-block">Edit</button>
                    </router-link>
                </td>
                <td>
                    <button @click="deleteTicket(ticket.id)" type="button" class="btn btn-danger btn-user btn-block">Delete</button>
                </td>
            </tr>
  </tbody>
</table>
</div>
</template>
<script>
import dataService from '../../services/dataService';
    export default {
        data() {
            return {
                tickets: []
            }
        },
        methods: {
            getTickets() {
                dataService.getTickets()
                .then(result => this.tickets = result.data.data)
                .catch(dataService.handleError);
            },
            deleteTicket(id) {
                if(confirm('Sure?')){
                    dataService.deleteTicket(id).
                    then(result => {
                        if(result.status === 200 && result.data.code === 0) {
                            this.getTickets();
                        }
                    }).
                    catch(dataService.handleError);
                }
            }
        },
        mounted() {
            this.getTickets();
        }
    }
</script>
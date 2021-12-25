export default {
    host: 'http://localhost:5001',
    api: {
        generateToken: '/api/v1/Token',
        getTickets: '/api/v1/Ticket',
        createTicket: '/api/v1/Ticket',
        deleteTicket: '/api/v1/Ticket',
        updateTicket: '/api/v1/Ticket',
        getTicketStatus: '/api/v1/Option/TicketStatus',
        getAccountInfo: '/api/v1/Account',
        getTicket: '/api/v1/Ticket',
        resolveTicket: '/api/v1/Ticket',
        logout: '/api/v1/Token/Logout'
    },
    permissions: {
        create: 'Create',
        edit: 'Edit',
        delete: 'Delete',
        resolve: 'Resolve'
    },
    token: 'token',
    Permissions: 'permissions'
}
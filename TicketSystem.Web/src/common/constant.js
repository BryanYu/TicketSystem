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
        logout: '/api/v1/Token/Logout',
        getTicketType: '/api/v1/Option/TicketType'
    },
    permissions: {
        create: 1,
        edit: 2,
        delete: 3,
        resolve: 4
    },
    ticketType: {
        bug: 1,
        featureRequest: 2
    },
    sesstionStorageKey: {
        token: 'token',
        permissions: 'permissions',
        ticketTypes: 'ticketTypes'
    }
}
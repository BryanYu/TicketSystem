import axios from "axios"
import constant from "../common/constant"
const instance = axios.create({
    baseURL: constant.host,
    headers: {
        'content-type': 'application/json'
    }
})

instance.interceptors.response.use(response => {
    return response
}, error => {
    if (error.config.globalErrorCatch === false) {
        return Promise.reject(error);
    }
    switch (error.response.status) {
        case 401:
            alert("UnAuthorized, Redirect to Login Page");
            window.location.href = '/Login';
            break;
        default:
            alert(this.response.data.message);
    }
    return Promise.reject(error);
})


function login(account, password) {
    return instance.post(constant.api.generateToken, {
        account: account,
        password: password
    }, {
        globalErrorCatch: false
    });
}

function getTickets() {
    setToken();
    return instance.get(constant.api.getTickets);
}

function getAccountInfo() {
    setToken();
    return instance.get(constant.api.getAccountInfo);
}

function createTicket(data) {
    setToken();
    return instance.post(constant.api.createTicket, data);
}

function getTicket(id) {
    setToken();
    return instance.get(constant.api.getTicket + '/' + id);
}

function deleteTicket(id) {
    setToken();
    return instance.delete(constant.api.getTicket + '/' + id);
}

function updateTicket(id, data) {
    setToken();
    return instance.put(constant.api.updateTicket + '/' + id, data);
}

function getTicketStatus() {
    setToken();
    return instance.get(constant.api.getTicketStatus);
}

function logout() {
    setToken();
    return instance.post(constant.api.logout, null, {
        globalErrorCatch: false
    });
}

function handlerSuccess(response) {
    if (response.status === 200 && response.data.code === 0) {
        alert('Sucess');
    }
}

function setToken() {
    var token = sessionStorage.getItem(constant.token);
    instance.defaults.headers.common['Authorization'] = 'Bearer ' + token;
}



export default {
    login: login,
    getTickets: getTickets,
    createTicket: createTicket,
    getTicket: getTicket,
    deleteTicket: deleteTicket,
    getAccountInfo: getAccountInfo,
    updateTicket: updateTicket,
    getTicketStatus: getTicketStatus,
    logout: logout,
    handlerSuccess: handlerSuccess

}
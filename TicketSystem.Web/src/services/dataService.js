import axios from "axios"
import constant from "../common/constant"
const instance = axios.create({
    baseURL: constant.host,
    headers: {
        'content-type': 'application/json'
    }
})

function login(account, password) {
    return instance.post(constant.api.generateToken, {
        account: account,
        password: password
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

function handlerSuccess(response) {
    if (response.status === 200 && response.data.code === 0) {
        alert('Sucess');
    }
}

function handleError(error) {
    alert(error);
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
    handleError: handleError,
    handlerSuccess: handlerSuccess

}
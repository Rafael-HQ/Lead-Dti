import axios from 'axios';

const api = axios.create({
    baseURL: "http://localhost:5260"
});

export default api;

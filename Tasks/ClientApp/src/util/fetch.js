import axiosClient from "axios";
import { authService } from "../providers/AuthProvider";

axiosClient.interceptors.request.use(function (config) {
    const token = authService.getToken()
    config.headers.Authorization = `Bearer ${token}`;

    return config;
});

export function request(url, options) {
    return axiosClient({
        url,
        ...options
    })
}
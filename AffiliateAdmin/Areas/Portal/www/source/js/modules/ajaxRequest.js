import axios from 'axios';
import { resolveClientUrl } from './utils';

let _xsrfToken = null;

const getXsrfToken = () => {
    if (!_xsrfToken) {
        _xsrfToken = document.querySelector("input[name=__RequestVerificationToken]").value;
    }

    return _xsrfToken;
}

axios.interceptors.response.use(response => response,
    error => {
        const { response } = error;
        if (response) {
            const { data } = response;
            const status = data && data.ErrCode || response.status;

            switch (status) {
                case 401:
                case 403:
                    alert("You are not authorized on this function.");
                    top.location.href = resolveClientUrl("");
                    break;
                case 500:
                    alert("An error occurred while processing your request");
                    break;
                default:
                    break;
            }
        }
    });

export const post = (relativeUrl, params) => {
    const url = resolveClientUrl(relativeUrl);
    return axios.post(url, params, {
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
            '__RequestVerificationToken': getXsrfToken()
        }
    });
}
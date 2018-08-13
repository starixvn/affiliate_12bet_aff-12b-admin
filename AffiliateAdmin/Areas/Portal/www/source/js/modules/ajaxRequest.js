import axios from 'axios';
import { resolveClientUrl } from './utils';

let _xsrfToken = null;

const getXsrfToken = () => {
    if (!_xsrfToken) {
        _xsrfToken = document.querySelector("input[name=__RequestVerificationToken]").value;
    }

    return _xsrfToken;
}

export const post = (relativeUrl, params) => {
    const url = resolveClientUrl(relativeUrl);
    return axios.post(url, params, {
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
            '__RequestVerificationToken': getXsrfToken()
        }
    });
}
import { post } from '../../../../modules/ajaxRequest';

export const getPaymentRequestDetail = async (param) => {
    const url = 'Portal/PaymentRequest/GetPaymentRequestDetail';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updatePaymentRequest = async (param) => {
    const url = 'Portal/PaymentRequest/UpdatePaymentRequest';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}
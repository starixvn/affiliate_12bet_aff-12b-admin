import moment from 'moment';

export const getNowString = () => {
    const now = new Date();
    const nowByHour = new Date(now.getFullYear(), now.getMonth(), now.getDate(), now.getHours());

    return moment(nowByHour).format('YYYY/MM/DD HH:mm:ss');
}
import { globalResource, resource as resources, statusList, pending } from '../../common/modules/constants/paymentRequestUpdate';

export { globalResource, statusList, pending }

export const headerText = {
    Index: '#',
    TrxnId: 'Trxn.ID',
    Creator: 'Creator'
}

export const resource = {
    SearchBy: 'Search By',

    ...resources
}

export const searchByList = ['User Name', 'Others']
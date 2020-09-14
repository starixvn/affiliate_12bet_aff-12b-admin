import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const resource = {
    Index: globalResource.Index,
    UserName: 'User Name',
    FirstName: 'First Name',
    LastName: 'Last Name',
    Creator: 'Creator',
    LastLoginDate: 'Last Login Date',
    LastLoginIp: 'Last Login Ip',
    Role: 'Role',
    Action: 'Action',
    Select: 'Select:',
    None: 'None',
    CreateNew: 'Create New',
    Search: globalResource.Search,
    Delete: globalResource.Delete
}

export const messages = {
    PleaseSelect: 'Please select items to delete',
    AreYouSureDeleteItems: 'Are you sure? <br /> By confirm this action, <strong class="text-danger">{0}</strong> users will be deleted?',
    DeleteItemsSuccessfully: 'Delete User(s) Successfully',
}
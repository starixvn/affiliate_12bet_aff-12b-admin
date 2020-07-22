import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const resource = {
    Index: '#',
    UserName: 'User Name',
    LoggedIn: 'Logged In',
    LastRequest: 'Last Request',
    IP: 'IP',
    Role: 'Role',
    Select: 'Select:',
    None: 'None',
    All: globalResource.All,
    Add: globalResource.Add,
    Search: globalResource.Search,
    Delete: globalResource.Delete
}

export const messages = {
    PleaseSelect: 'Please select items to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected user(s)?',
    DeleteItemsSuccessfully: 'Delete User(s) Successfully',
    AreYouWant: 'Are you sure you want to update this user?'
}
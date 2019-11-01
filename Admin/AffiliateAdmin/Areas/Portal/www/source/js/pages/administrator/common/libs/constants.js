import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const listAlPhabet = ['0-9','A','B','C','D','E','F','G','H','I','J','K','L','M', 
    'N','O','P','Q','R','S','T','U','V','W','X','Y','Z'];

export const headerText = {
    Index: '#',
    UserName: 'User Name',
    FirstName: 'First Name',
    LastName: 'Last Name',
    Status: 'Status',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    Policy: 'Policy',
    Department: 'Department',
    Password: 'Password'
}

export const defaultState = {
    selectedUserId: 0,
    isOpen: false,
    isShowUserPopup: false,
    isShowPolicyPopup: false,
    isShowDepartmentPopup: false,
    isShowPasswordPopup: false,
    message: '',
    isConfirmPopup: true
}
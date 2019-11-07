import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const MaxLength = 19;

export const listAlPhabet = ['0-9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

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
    isInsertExistedAcc: false,
    selectedUserId: 0,
    selectedGroupId: 0,
    userName: '',
    groupName: '',
    isOpen: false,
    isShowUserPopup: false,
    isShowPolicyPopup: false,
    isShowDepartmentPopup: false,
    isShowPasswordPopup: false,
    message: '',
    isConfirmPopup: true
}

export const convertCheckedPolicies = (checkedPolicies, selectedId, isAccountPolicy, isGetByIdList) => {
    if (isGetByIdList) {
        const isList = selectedId.split(',');
        return isList.reduce((accumulator, id) => {
            return accumulator = accumulator + getStringConvertCheckedPolicies(checkedPolicies, id, isAccountPolicy);
        }, '');
    }

    return getStringConvertCheckedPolicies(checkedPolicies, selectedId, isAccountPolicy);
}
import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    DepositUserName: 'Deposit User Name',
    CheckStatus: 'Check Status',
    Index: '#',
    UserName: 'User Name',
    IbcUserName: 'IBC User Name',
    FirstName: 'First Name',
    LastName: 'Last Name',
    IBCCustID: 'IBCCustID',
    CreatedDate: 'Created Date',
    ActivateAll: 'Activate All'
}

export const getUnexistUserNames = (activedList, unactiveList, userNames) => {
    const existedUserNameList = activedList.concat(unactiveList).map(item => item.UserName);
    const userNameList = userNames.split(',').filter(item => item !== '');
    const unexistUserNameList = userNameList.filter(userName => existedUserNameList.indexOf(userName) === -1 );

    console.log(activedList);
    console.log(unactiveList);
    console.log(existedUserNameList);
    console.log(userNameList);
    console.log(unexistUserNameList);
    console.log(unexistUserNameList.toString());

    return unexistUserNameList.toString();
}
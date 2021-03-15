import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';
import PersonalPermissionTable from '../PersonalPermissionTable';
import { messages } from '../../libs/constants';

describe('admin info - components - Content', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const roleList = {
        1: "Management",
        2: "Exe. Settlement",
        3: "Exe. Risk Team",
        4: "Exe. Marketing",
        5: "Asst. Marketing"
    }

    const permissionListByRole = [
        {
            AllParentIds: "1",
            DisplayName: "Home page",
            FuncId: 1,
            FuncName: "[Home]",
            IsHost: true,
            IsRead: true,
            IsRoleHost: true,
            IsRoleRead: true,
            ParentId: 1,
            RoleName: "Exe. Settlement"
        },
        {
            AllParentIds: "22",
            DisplayName: "Member Menu",
            FuncId: 22,
            FuncName: "[AffMember]",
            IsHost: false,
            IsRead: true,
            IsRoleHost: false,
            IsRoleRead: true,
            ParentId: 22,
            RoleName: "Exe. Settlement"
        },
        {
            AllParentIds: "22",
            DisplayName: "Member",
            FuncId: 23,
            FuncName: "[AffMemberDetail]",
            IsHost: false,
            IsRead: true,
            IsRoleHost: false,
            IsRoleRead: true,
            ParentId: 22,
            RoleName: "Exe. Settlement"
        },
        {
            AllParentIds: "22",
            DisplayName: "Active member",
            FuncId: 24,
            FuncName: "[ActiveMembers]",
            IsHost: false,
            IsRead: false,
            IsRoleHost: false,
            IsRoleRead: false,
            ParentId: 22,
            RoleName: "Exe. Settlement"
        },
        {
            AllParentIds: "22,24",
            DisplayName: "isRecursive",
            FuncId: 25,
            FuncName: "[Recursive]",
            IsHost: false,
            IsRead: false,
            IsRoleHost: false,
            IsRoleRead: false,
            ParentId: 24,
            RoleName: "Exe. Settlement"
        }
    ];

    const adminInfo = {
        FirstName: "Hubert",
        IsActive: true,
        IsRoleFunctionDefault: false,
        IsRoleMarketDefault: false,
        LastName: "Duong",
        RoleId: 1,
        UserId: 10000002,
        UserName: "Hubert"
    }

    const marketList = [
        { RoleId: 0, MarketId: "en-US", MarketName: "English", HasPermission: true },
        { RoleId: 0, MarketId: "vi-VN", MarketName: "Vietnamese", HasPermission: true },
        { RoleId: 0, MarketId: "th-TH", MarketName: "Thai", HasPermission: false }
    ];

    const userInfo = {
        AdminInfo: adminInfo,
        MarketList: marketList,
        UserPermissionList: permissionListByRole
    }

    const defaultProps = {
        userId: 10000002,
        userName: 'AAHubert',
        hasWriteAdminRoleClaim: true,
        roleList,
        getRoleList: jest.fn(() => roleList),
        fetchData: jest.fn(() => userInfo),
        toggleLoader: jest.fn(),
        setPermissionListByRole: jest.fn(),
        permissionListByRole
    };

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.instance().setState({ isSuccess: true, isChangingPassword: true });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with no permission', () => {
        const props = Object.assign({}, defaultProps, { hasWriteAdminRoleClaim: false, fetchData: jest.fn(() => null) });
        const result = shallow(<Content {...props} />);
        result.instance().setState({ messagePopup: "test" });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with create new', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.instance().setState({ isCreateNew: true, marketListByRole: marketList });

        expect(result).toMatchSnapshot();
    });

    it('render correctly after click submit when create new', () => {
        const props = Object.assign({}, defaultProps, { marketListByRole: [] });
        const result = shallow(<Content {...props} />);
        result.instance().setState({ isCreateNew: true, isSuccess: false, isClickedSubmit: true, marketListByRole: [] });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with edit', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.instance().setState({ isEdit: true, marketListByRole: marketList });

        expect(result).toMatchSnapshot();
    });

    it('render correctly after click submit when edit', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.instance().setState({ isEdit: true, isSuccess: false, isClickedSubmit: true, marketListByRole: [] });

        expect(result).toMatchSnapshot();
    });

    it('setState - should be called when click edit button', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.setState = jest.fn();

        result.find("Button").at(1).simulate("click");

        expect(result.setState).toHaveBeenCalledWith({ isEdit: true });
    });

    it('_toggleChangingPassword - should be called when click change button', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._toggleChangingPassword = jest.fn();

        result.find("Button").at(2).simulate("click");

        expect(instance._toggleChangingPassword).toHaveBeenCalledWith(true);
    });

    it('_handleChangeStatus - should be called when click checkbox', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._handleChangeStatus = jest.fn();

        result.find("input").at(0).simulate("change");

        expect(instance._handleChangeStatus).toHaveBeenCalledWith(false);
    });

    it('_handleCancel - should be called when click cancel button', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isEdit: true });
        instance._handleCancel = jest.fn();

        result.find("Button").at(0).simulate("click");

        expect(instance._handleCancel).toHaveBeenCalledWith(false);
    });

    it('_handleSubmit - should be called when click submit button', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isEdit: true });
        instance._handleSubmit = jest.fn();

        result.find("Button").at(1).simulate("click");

        expect(instance._handleSubmit).toHaveBeenCalledWith(false);
    });

    it('_handlePressEnter - should be called when press enter in username input', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isEdit: true });
        instance._handlePressEnter = jest.fn();

        result.find('Input[name="newUserName"]').simulate("keyPress");

        expect(instance._handlePressEnter).toHaveBeenCalled();
    });

    it('_handlePressEnter - should be called when press enter in firstName input', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isEdit: true });
        instance._handlePressEnter = jest.fn();

        result.find('Input[name="firstName"]').simulate("keyPress");

        expect(instance._handlePressEnter).toHaveBeenCalled();
    });

    it('_handlePressEnter - should be called when press enter in lastName input', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isEdit: true });
        instance._handlePressEnter = jest.fn();

        result.find('Input[name="lastName"]').simulate("keyPress");

        expect(instance._handlePressEnter).toHaveBeenCalled();
    });

    it('_handlePressEnter - should be called when press enter in newPassword input', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isCreateNew: true });
        instance._handlePressEnter = jest.fn();

        result.find('Input[name="newPassword"]').simulate("keyPress");

        expect(instance._handlePressEnter).toHaveBeenCalled();
    });

    it('setState - should be called when call handleSetRoleFunctionDefault in PersonalPermissionTable', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isEdit: true });
        instance.setState = jest.fn();

        result.find(PersonalPermissionTable).props().handleSetRoleFunctionDefault();

        expect(instance.setState).toHaveBeenCalledWith({ isRoleFunctionDefault: true });
    });

    it('_handleChange - should call setState', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleChange({ target: { name: 'abc', value: 123 } });

        expect(instance.setState).toHaveBeenCalledWith({ abc: 123 });
    });

    it('_handleChangeRole - should call setState, getPermissionListByRole and _handleUpdateMarketList', () => {
        const props = Object.assign({}, defaultProps, { getPermissionListByRole: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._handleUpdateMarketList = jest.fn();
        instance.setState = jest.fn();

        instance._handleChangeRole({ target: { value: 2 } });

        expect(instance.setState).toHaveBeenCalledWith({ role: 2 });
        expect(props.getPermissionListByRole).toHaveBeenCalledWith(2);
        expect(instance._handleUpdateMarketList).toHaveBeenCalledWith(2);
    });

    it('_handleChangeRole - should call setState, setPermissionListByRole', () => {
        const props = Object.assign({}, defaultProps, { setPermissionListByRole: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState({ initialRole: 1 });
        instance._handleUpdateMarketList = jest.fn();
        instance.setState = jest.fn();

        instance._handleChangeRole({ target: { value: 1 } });

        expect(instance.setState).toHaveBeenCalledTimes(2);
        expect(instance.setState.mock.calls[0][0]).toEqual({ role: 1 });
        expect(instance.setState.mock.calls[1][0]).toEqual({ marketListByRole: null });
        expect(props.setPermissionListByRole).toHaveBeenCalledWith(null);
        expect(instance._handleUpdateMarketList).not.toHaveBeenCalled();
    });

    it('_handleUpdateMarketList - should call setState, getMarketListByRole', async () => {
        const props = Object.assign({}, defaultProps, { getMarketListByRole: jest.fn(() => marketList) });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._handleUpdateMarketList(1);

        expect(instance.setState).toHaveBeenCalledWith({ marketListByRole: marketList });
        expect(props.getMarketListByRole).toHaveBeenCalledWith(1, false);
    });

    it('_handleSubmit - should call setState', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        instance._handleSubmit(false);

        expect(instance.setState).toHaveBeenCalledWith({ isClickedSubmit: true });
        expect(instance._handleShowAlert).not.toHaveBeenCalled();
    });

    it('_handleSubmit - should call setState, createUser when create successfully', async () => {
        const props = Object.assign({}, defaultProps, { createUser: jest.fn(() => ({ IsSuccessful: true, Message: "" })), userId: 0 });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit(true);

        expect(instance.setState).toHaveBeenCalledWith({ isClickedSubmit: true });
        expect(props.createUser).toHaveBeenCalled();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Create new user is successful!");
    });

    it('_handleSubmit - should call setState, createUser when create failed', async () => {
        const props = Object.assign({}, defaultProps, { createUser: jest.fn(() => ({ IsSuccessful: false, Message: "Failed!" })), userId: 0 });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit(true);

        expect(instance.setState).toHaveBeenCalledWith({ isClickedSubmit: true });
        expect(props.createUser).toHaveBeenCalled();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Failed!");
    });

    it('_handleSubmit - should call setState, createUser when create errored', async () => {
        const props = Object.assign({}, defaultProps, { createUser: jest.fn(), userId: 0 });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit(true);

        expect(instance.setState).toHaveBeenCalledWith({ isClickedSubmit: true });
        expect(props.createUser).toHaveBeenCalled();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Create new user is failed!");
    });

    it('_handleSubmit - should call setState, updateUser when edit successfully', async () => {
        const props = Object.assign({}, defaultProps, { updateUser: jest.fn(() => ({ IsSuccessful: true, Message: "" })) });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit(true);

        expect(instance.setState).toHaveBeenCalledWith({ isClickedSubmit: true });
        expect(props.updateUser).toHaveBeenCalled();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Update user is successful!");
    });


    it('_handleSubmit - should call setState, updateUser when edit failed', async () => {
        const props = Object.assign({}, defaultProps, { updateUser: jest.fn(() => ({ IsSuccessful: false, Message: "Failed!" })) });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit(true);

        expect(instance.setState).toHaveBeenCalledWith({ isClickedSubmit: true });
        expect(props.updateUser).toHaveBeenCalled();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Failed!");
    });

    it('_handleSubmit - should call setState, updateUser when edit errored', async () => {
        const props = Object.assign({}, defaultProps, { updateUser: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit(true);

        expect(instance.setState).toHaveBeenCalledWith({ isClickedSubmit: true });
        expect(props.updateUser).toHaveBeenCalled();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Update user is failed!");
    });

    it('_handleShowAlert - should call setState', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn((obj, func) => func && func());
        window.setTimeout = jest.fn((func, time) => func && func());
        instance._hideMessage = jest.fn();

        instance._handleShowAlert(false, "test");

        expect(instance.setState).toHaveBeenCalledWith({ isShowMessage: true, isSuccess: false, message: "test" }, expect.any(Function));
        expect(instance._hideMessage).toHaveBeenCalledWith(false);
    });

    it('_hideMessage - should call setState', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._hideMessage(false);

        expect(instance.setState).toHaveBeenCalledWith({ isShowMessage: false });
    });

    it('_hideMessage - should call setState and redirectToAdminList', () => {
        const props = Object.assign({}, defaultProps, { redirectToAdminList: jest.fn(), userId: 0 });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._hideMessage(true);

        expect(instance.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.redirectToAdminList).toHaveBeenCalled();
    });

    it('_hideMessage - should call setState and _handleFetchData', () => {
        const props = Object.assign({}, defaultProps, { redirectToAdminList: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleFetchData = jest.fn();

        instance._hideMessage(true);

        expect(instance.setState).toHaveBeenCalledTimes(2);
        expect(instance.setState.mock.calls[0][0]).toEqual({ isShowMessage: false });
        expect(instance.setState.mock.calls[1][0]).toEqual({ isClickedSubmit: false, isEdit: false });
        expect(props.redirectToAdminList).not.toHaveBeenCalled();
        expect(instance._handleFetchData).toHaveBeenCalledWith(defaultProps.userId);
    });

    it('_handlePressEnter - should call _handleSubmit', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._handleSubmit = jest.fn();

        instance._handlePressEnter({ charCode: 13, preventDefault: jest.fn() }, true);

        expect(instance._handleSubmit).toHaveBeenCalledWith(true);
    });

    it('_handlePressEnter - should not call _handleSubmit', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._handleSubmit = jest.fn();

        instance._handlePressEnter({ charCode: 11 }, true);

        expect(instance._handleSubmit).not.toHaveBeenCalled();
    });

    it('_handleCancel - should call redirectToAdminList', () => {
        const props = Object.assign({}, defaultProps, { redirectToAdminList: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();

        instance._handleCancel(true);

        expect(props.redirectToAdminList).toHaveBeenCalled();
    });

    it('_handleCancel - should call setPermissionListByRole', () => {
        const props = Object.assign({}, defaultProps, { redirectToAdminList: jest.fn(), setPermissionListByRole: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState({ initialUserInfo: adminInfo });
        instance._handleCancel(false);

        expect(props.setPermissionListByRole).toHaveBeenCalled();
        expect(props.redirectToAdminList).not.toHaveBeenCalled();
    });

    it('_toggleChangingPassword - should call setState', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._toggleChangingPassword(false);

        expect(instance.setState).toHaveBeenCalledWith({ isChangingPassword: false });
    });

    it('_handleDeleteUser - should call setState', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleDeleteUser();

        expect(instance.setState).toHaveBeenCalledWith({ isConfirmPopup: true, isDeleteUser: true, messagePopup: `Are you sure? <br /> By confirm this action, user <strong className=\"text-danger\">AAHubert</strong> will be deleted?` });
    });

    it('_handleChangeStatus - should call setState when deactivated', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleChangeStatus(false);

        expect(instance.setState).toHaveBeenCalledWith({ isConfirmPopup: true, isDeleteUser: false, messagePopup: `Are you sure? <br /> By confirm this action, user <strong className=\"text-danger\">AAHubert</strong> will be deactivated?` });
    });

    it('_handleChangeStatus - should call setState when activated', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleChangeStatus(true);

        expect(instance.setState).toHaveBeenCalledWith({ isConfirmPopup: true, isDeleteUser: false, messagePopup: `Are you sure? <br /> By confirm this action, user <strong className=\"text-danger\">AAHubert</strong> will be activated?` });
    });

    it('_handleCloseNotificationPopup - should call setState and _deleteUser', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isDeleteUser: true });
        instance.setState = jest.fn();
        instance._deleteUser = jest.fn();

        instance._handleCloseNotificationPopup(true);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: '' });
        expect(instance._deleteUser).toHaveBeenCalledWith(defaultProps.userId);
    });

    it('_handleCloseNotificationPopup - should call setState and _updateStatus', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._deleteUser = jest.fn();
        instance._updateStatus = jest.fn();

        instance._handleCloseNotificationPopup(true);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: '' });
        expect(instance._deleteUser).not.toHaveBeenCalled();
        expect(instance._updateStatus).toHaveBeenCalledWith(defaultProps.userId, true);
    });

    it('_handleCloseNotificationPopup - should call setState and redirectToAdminList', () => {
        const props = Object.assign({}, defaultProps, { redirectToAdminList: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState({ isAfterDeleted: true });
        instance.setState = jest.fn();
        instance._deleteUser = jest.fn();
        instance._updateStatus = jest.fn();

        instance._handleCloseNotificationPopup(false);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: '' });
        expect(instance._deleteUser).not.toHaveBeenCalled();
        expect(instance._updateStatus).not.toHaveBeenCalled();
        expect(props.redirectToAdminList).toHaveBeenCalled();
    });

    it('_handleCloseNotificationPopup - should call only setState', () => {
        const props = Object.assign({}, defaultProps, { redirectToAdminList: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._deleteUser = jest.fn();
        instance._updateStatus = jest.fn();

        instance._handleCloseNotificationPopup(false);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: '' });
        expect(instance._deleteUser).not.toHaveBeenCalled();
        expect(instance._updateStatus).not.toHaveBeenCalled();
        expect(props.redirectToAdminList).not.toHaveBeenCalled();
    });

    it('_handleChangeMarket - should call setState and _handleUpdateMarketList', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn((obj, func) => func && func());
        instance._handleUpdateMarketList = jest.fn();

        const item = { MarketId: "SetByDefault", MarketName: "Set to Role's Default", HasPermission: true };
        instance._handleChangeMarket(item, '', true);

        expect(instance.setState).toHaveBeenCalledWith({ itemDefault: item }, expect.any(Function));
        expect(instance._handleUpdateMarketList).toHaveBeenCalledWith(0);
    });

    it('_handleChangeMarket - should call setState but not call _handleUpdateMarketList', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ marketListByRole: marketList });
        instance.setState = jest.fn();
        instance._handleUpdateMarketList = jest.fn();

        const item = { MarketId: "vi-VN", MarketName: "VietNamese", HasPermission: false };
        instance._handleChangeMarket(item, 'en-US', false);

        expect(instance.setState).toHaveBeenCalledWith({
            marketListByRole: [
                { MarketId: "en-US", MarketName: "English", HasPermission: true },
                { MarketId: "vi-VN", MarketName: "Vietnamese", HasPermission: false },
                { MarketId: "th-TH", MarketName: "Thai", HasPermission: false }
            ] });
        expect(instance._handleUpdateMarketList).not.toHaveBeenCalled();
    });

    it('_deleteUser - should call deleteUsers, setState when success', async () => {
        const props = Object.assign({}, defaultProps, { deleteUsers: jest.fn(() => ({ IsSuccessful: true, Message: "" })) });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._deleteUser(123);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: 'User has been deleted successfully!', isConfirmPopup: false, isAfterDeleted: true });
        expect(props.deleteUsers).toHaveBeenCalledWith(123);
    });

    it('_deleteUser - should call deleteUsers, setState when failed', async () => {
        const props = Object.assign({}, defaultProps, { deleteUsers: jest.fn(() => ({ IsSuccessful: false, Message: "failed!" })) });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._deleteUser(123);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: 'failed!', isConfirmPopup: false, isAfterDeleted: true });
        expect(props.deleteUsers).toHaveBeenCalledWith(123);
    });

    it('_deleteUser - should call deleteUsers, setState when errored', async () => {
        const props = Object.assign({}, defaultProps, { deleteUsers: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._deleteUser(123);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: messages.AnError, isDeleteUser: false, isConfirmPopup: false });
        expect(props.deleteUsers).toHaveBeenCalledWith(123);
    });

    it('_updateStatus - should call updateStatus, setState when success', async () => {
        const props = Object.assign({}, defaultProps, { updateStatus: jest.fn(() => ({ IsSuccessful: true, Message: "" })) });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._updateStatus(123, false);

        expect(instance.setState.mock.calls[1][0]).toEqual({ isActive: true });
        expect(instance.setState.mock.calls[2][0]).toEqual({ messagePopup: messages.UpdateStatusSuccessfully, isConfirmPopup: false });
        expect(props.updateStatus).toHaveBeenCalledWith({ userId: 123, isClosed: false });
    });

    it('_updateStatus - should call updateStatus, setState when failed', async () => {
        const props = Object.assign({}, defaultProps, { updateStatus: jest.fn(() => ({ IsSuccessful: false, Message: "failed!" })) });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._updateStatus(123, false);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: "failed!", isConfirmPopup: false });
        expect(props.updateStatus).toHaveBeenCalledWith({ userId: 123, isClosed: false });
    });

    it('_updateStatus - should call updateStatus, setState when errored', async () => {
        const props = Object.assign({}, defaultProps, { updateStatus: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._updateStatus(123, false);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: messages.AnError, isConfirmPopup: false });
        expect(props.updateStatus).toHaveBeenCalledWith({ userId: 123, isClosed: false });
    });
});
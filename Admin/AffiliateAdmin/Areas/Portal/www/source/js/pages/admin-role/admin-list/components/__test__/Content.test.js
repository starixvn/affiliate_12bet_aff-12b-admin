import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';
import MyLink from '../../../../common/components/MyLink';
import { messages } from '../../libs/constants';

describe('admin list - components - Content', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const param = {
        userName: ''
    }

    const gridData = [
        {
            Creator: "AAsa",
            FirstName: "Camy",
            IsActive: true,
            LastLoginDate: "2021-03-16T05:53:01",
            LastLoginDateText: "03/16/2021 05:53:01",
            LastLoginIp: "::1",
            LastName: "Vương",
            RoleName: "Management",
            UserId: 10000028,
            UserName: "AACamy"
        },
        {
            Creator: "",
            FirstName: "Hubert",
            IsActive: false,
            LastLoginDate: "2021-03-16T04:12:06",
            LastLoginDateText: "03/16/2021 04:12:06",
            LastLoginIp: "::1",
            LastName: "Duong",
            RoleName: "Management",
            UserId: 10000002,
            UserName: "AAHubert"
        }
    ]

    const checkedUsers = {
        10000028: true,
        10000002: false
    }

    const defaultProps = {
        isBack: false,
        param: null,
        gridData,
        checkedUsers,
        hasWriteAdminRoleClaim: true,
        shouldResetScrollPosition: true
    }

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not permission', () => {
        const props = Object.assign({}, defaultProps, { hasWriteAdminRoleClaim: false });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is null', () => {
        const props = Object.assign({}, defaultProps, { gridData: null });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is empty and not has permission', () => {
        const props = Object.assign({}, defaultProps, { gridData: [], hasWriteAdminRoleClaim: false });
        const result = shallow(<Content {...props} />);
        result.instance().setState({ message: 'test' });

        expect(result).toMatchSnapshot();
    });

    it('_onCheckItem - should be called when click checkbox', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._onCheckItem = jest.fn();

        const dataRow = shallow(<div>{instance._buildDataRow(gridData[0], 1, true)}</div>);

        dataRow.find("input").simulate("change", { target: { checked: true } });
        expect(instance._onCheckItem).toHaveBeenCalledWith(true, 10000028);
    });

    it('_handleRedirectToUserDetail - should be called when click link', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._handleRedirectToUserDetail = jest.fn();

        const dataRow = shallow(<div>{instance._buildDataRow(gridData[0], 1, true)}</div>);

        dataRow.find(MyLink).props().onClick();
        expect(instance._handleRedirectToUserDetail).toHaveBeenCalledWith(10000028, 'AACamy');
    });

    it('_handleShowPermission - should be called when click eye icon', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowPermission = jest.fn();

        const dataRow = shallow(<div>{instance._buildDataRow(gridData[0], 1, true)}</div>);

        dataRow.find("Button").at(0).simulate("click");
        expect(instance._handleShowPermission).toHaveBeenCalledWith(10000028, 'AACamy');
    });

    it('_handleDeleteUser - should be called when click trash icon', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance._handleDeleteUser = jest.fn();

        const dataRow = shallow(<div>{instance._buildDataRow(gridData[0], 1, true)}</div>);

        dataRow.find("Button").at(1).simulate("click");
        expect(instance._handleDeleteUser).toHaveBeenCalledWith(10000028, 'AACamy');
    });

    it('componentDidUpdate - should call fetchData', () => {
        const props = Object.assign({}, defaultProps, { param, fetchData: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._onCheckItem = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(props.fetchData).toHaveBeenCalledWith(param);
    });

    it('componentDidUpdate - should call fetchData with isBack', () => {
        const props = Object.assign({}, defaultProps, { isBack: true, fetchData: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance._onCheckItem = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(props.fetchData).toHaveBeenCalledWith(null);
    });

    it('_onCheckItem - should call fetchData', () => {
        const props = Object.assign({}, defaultProps, { setCheckedUsers: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();

        instance._onCheckItem(false, 10000028);

        expect(props.setCheckedUsers).toHaveBeenCalledWith({ 10000028: false, 10000002: false });
    });

    it('_handleCloseNotificationPopup - should call setState and _deleteUser', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ selectedUserId: 10000028 });
        instance.setState = jest.fn();
        instance._deleteUser = jest.fn();

        instance._handleCloseNotificationPopup(true);

        expect(instance.setState).toHaveBeenCalledWith({ message: '' });
        expect(instance._deleteUser).toHaveBeenCalledWith(10000028);
    });

    it('_handleCloseNotificationPopup - should call setState but not call _deleteUser', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._deleteUser = jest.fn();

        instance._handleCloseNotificationPopup(true);

        expect(instance.setState).toHaveBeenCalledWith({ message: '' });
        expect(instance._deleteUser).not.toHaveBeenCalled();
    });

    it('_handleDeleteUser - should call setShouldResetScrollPosition and setState', async () => {
        const props = Object.assign({}, defaultProps, { setShouldResetScrollPosition: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._handleDeleteUser(10000028, 'AACamy');

        expect(instance.setState).toHaveBeenCalledWith({ message: 'Are you sure? <br /> By confirm this action, user <strong className=\"text-danger\">AACamy</strong> will be deleted?', isConfirmPopup: true, selectedUserId: 10000028 });
        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
    });

    it('_deleteUser - should call setState, fetchData when delete successfully', async () => {
        const props = Object.assign({}, defaultProps, { param, deleteUsers: jest.fn(() => ({ IsSuccessful: true, Message: "" })), fetchData: jest.fn(), toggleLoader: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._deleteUser(10000028);

        expect(instance.setState).toHaveBeenCalledWith({ message: 'User has been deleted successfully!', isConfirmPopup: false });
        expect(props.deleteUsers).toHaveBeenCalledWith(10000028);
        expect(props.fetchData).toHaveBeenCalledWith(param);
    });


    it('_deleteUser - should call setState but not call deleteUsers when delete failed', async () => {
        const props = Object.assign({}, defaultProps, { deleteUsers: jest.fn(() => ({ IsSuccessful: false, Message: "Failed!" })), fetchData: jest.fn(), toggleLoader: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._deleteUser(10000028);

        expect(props.deleteUsers).toHaveBeenCalledWith(10000028);
        expect(instance.setState).toHaveBeenCalledWith({ message: 'Failed!', isConfirmPopup: false });
        expect(props.fetchData).not.toHaveBeenCalled();
    });

    it('_deleteUser - should call setState, deleteUsers when delete errored', async () => {
        const props = Object.assign({}, defaultProps, { deleteUsers: jest.fn(), fetchData: jest.fn(), toggleLoader: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._deleteUser(10000028);

        expect(instance.setState).toHaveBeenCalledWith({ message: messages.AnError, isConfirmPopup: false });
        expect(props.deleteUsers).toHaveBeenCalledWith(10000028);
        expect(props.fetchData).not.toHaveBeenCalled();
    });

    it('_handleRedirectToUserDetail - should call redirectToUserDetail', () => {
        const props = Object.assign({}, defaultProps, { redirectToUserDetail: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();

        instance._handleRedirectToUserDetail(10000028, 'AACamy');

        expect(props.redirectToUserDetail).toHaveBeenCalledWith(10000028, 'AACamy');
    });

    it('_handleShowPermission - should call setUserInfo', () => {
        const props = Object.assign({}, defaultProps, { setUserInfo: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();

        instance._handleShowPermission(10000028, 'AACamy');

        expect(props.setUserInfo).toHaveBeenCalledWith({ UserId: 10000028, UserName: 'AACamy' });
    });
});
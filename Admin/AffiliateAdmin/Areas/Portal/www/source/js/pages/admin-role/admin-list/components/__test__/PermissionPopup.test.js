import React from 'react';
import { PermissionPopup } from '../PermissionPopup';
import { shallow } from 'enzyme';

describe('admin list - components - PermissionPopup', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const userInfo = { UserId: 10000002, UserName: "AAHubert" };
    const data = [
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

    const defaultProps = {
        userInfo
    };

    it('render correctly', () => {
        const result = shallow(<PermissionPopup {...defaultProps} />);
        result.instance().setState({ data });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with data null', () => {
        const result = shallow(<PermissionPopup {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with data empty', () => {
        const result = shallow(<PermissionPopup {...defaultProps} />);
        result.instance().setState({ data: [] });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with data has not child', () => {
        const result = shallow(<PermissionPopup {...defaultProps} />);
        result.instance().setState({
            data: [
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
                }
            ] });

        expect(result).toMatchSnapshot();
    });

    it("componentDidUpdate - should call _handleGetUserPermissionList", () => {
        const result = shallow(<PermissionPopup {...defaultProps} />);
        const instance = result.instance();
        instance._handleGetUserPermissionList = jest.fn();

        instance.componentDidUpdate({ userInfo: null });

        expect(instance._handleGetUserPermissionList).toHaveBeenCalledWith(userInfo.UserId);
    });

    it("_handleGetUserPermissionList - should call getUserPermissionList and setState", async () => {
        const props = Object.assign({}, defaultProps, { getUserPermissionList: jest.fn(() => data) });
        const result = shallow(<PermissionPopup {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._handleGetUserPermissionList(userInfo.UserId);

        expect(props.getUserPermissionList).toHaveBeenCalledWith(userInfo.UserId);
        expect(instance.setState).toHaveBeenCalledWith({ data });
    });

    it("_handleClosePopup - should call setUserInfo and setState", () => {
        const props = Object.assign({}, defaultProps, { setUserInfo: jest.fn() });
        const result = shallow(<PermissionPopup {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleClosePopup();

        expect(props.setUserInfo).toHaveBeenCalledWith(null);
        expect(instance.setState).toHaveBeenCalledWith({ data: null });
    });
});
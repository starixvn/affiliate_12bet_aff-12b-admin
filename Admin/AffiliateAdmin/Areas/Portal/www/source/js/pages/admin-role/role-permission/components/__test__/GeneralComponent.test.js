import React from 'react';
import { GeneralComponent } from '../GeneralComponent';
import { shallow } from 'enzyme';

describe('role permission - components - GeneralComponent', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const gridData = [{
        AllParentIds: "1",
        DisplayName: "Home page",
        FuncId: 1,
        FuncName: "[Home]",
        IsHost: true,
        IsRead: true,
        ParentId: 1,
        Policy: 3,
        RoleId: 1,
        RoleName: "Management"
    },
    {
        AllParentIds: "22",
        DisplayName: "Member Menu",
        FuncId: 22,
        FuncName: "[AffMember]",
        IsHost: true,
        IsRead: true,
        ParentId: 22,
        Policy: 3,
        RoleId: 1,
        RoleName: "Management"
    },
    {
        AllParentIds: "22",
        DisplayName: "Member",
        FuncId: 23,
        FuncName: "[AffMemberDetail]",
        IsHost: true,
        IsRead: true,
        ParentId: 22,
        Policy: 3,
        RoleId: 1,
        RoleName: "Management"
    },
    {
        AllParentIds: "22",
        DisplayName: "Active member",
        FuncId: 24,
        FuncName: "[ActiveMembers]",
        IsHost: true,
        IsRead: true,
        ParentId: 22,
        Policy: 3,
        RoleId: 1,
        RoleName: "Management"
    },
    {
        AllParentIds: "1",
        DisplayName: "Home page",
        FuncId: 1,
        FuncName: "[Home]",
        IsHost: true,
        IsRead: true,
        ParentId: 1,
        Policy: 3,
        RoleId: 2,
        RoleName: "Exe. Settlement"
    },
    {
        AllParentIds: "22",
        DisplayName: "Member Menu",
        FuncId: 22,
        FuncName: "[AffMember]",
        IsHost: false,
        IsRead: true,
        ParentId: 22,
        Policy: 0,
        RoleId: 2,
        RoleName: "Exe. Settlement"
    },
    {
        AllParentIds: "22",
        DisplayName: "Member",
        FuncId: 23,
        FuncName: "[AffMemberDetail]",
        IsHost: false,
        IsRead: true,
        ParentId: 22,
        Policy: 0,
        RoleId: 2,
        RoleName: "Exe. Settlement"
    },
    {
        AllParentIds: "22",
        DisplayName: "Active member",
        FuncId: 24,
        FuncName: "[ActiveMembers]",
        IsHost: false,
        IsRead: false,
        ParentId: 22,
        Policy: 0,
        RoleId: 2,
        RoleName: "Exe. Settlement"
    }];

    const roleList = [
        { RoleId: 1, RoleName: "Management" },
        { RoleId: 2, RoleName: "Exe. Settlement" }
    ];

    const defaultProps = {
        gridData,
        roleList,
        toggleLoader: jest.fn(),
        setGridData: jest.fn(),
        getPermissionInfoList: jest.fn(() => gridData),
        hasWriteAdminRoleClaim: true
    }

    it('render correctly', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not write permission', () => {
        const props = Object.assign({}, defaultProps, { hasWriteAdminRoleClaim: false });
        const result = shallow(<GeneralComponent {...props} />);

        expect(result).toMatchSnapshot();
    });
});
import React from 'react';
import { GeneralComponent } from '../GeneralComponent';
import { shallow } from 'enzyme';
import MyLink from '../../../../common/components/MyLink';
import { messages } from '../../libs/constants';

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
        AllParentIds: "22,24",
        DisplayName: "isRecursive",
        FuncId: 25,
        FuncName: "[Recursive]",
        IsHost: true,
        IsRead: true,
        ParentId: 24,
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
    },
    {
        AllParentIds: "22,24",
        DisplayName: "isRecursive",
        FuncId: 25,
        FuncName: "[Recursive]",
        IsHost: false,
        IsRead: false,
        ParentId: 24,
        Policy: 3,
        RoleId: 2,
        RoleName: "Exe. Settlement"
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
        RoleId: 3,
        RoleName: "Assistant"
    }];

    const roleList = [
        { RoleId: 1, RoleName: "Management" },
        { RoleId: 2, RoleName: "Exe. Settlement" },
        { RoleId: 3, RoleName: "Assistant" }
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
        result.setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not write permission', () => {
        const props = Object.assign({}, defaultProps, { hasWriteAdminRoleClaim: false });
        const result = shallow(<GeneralComponent {...props} />);
        result.setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with roleList is empty', () => {
        const props = Object.assign({}, defaultProps, { roleList: [] });
        const result = shallow(<GeneralComponent {...props} />);
        result.setState({ messagePopup: 'test', gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with expand a role', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        result.setState({ expandedRoleId: 2, gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with expand a role has not children', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        result.setState({ expandedRoleId: 3, gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with isEdit and expand a role', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        result.setState({ isEdit: true, expandedRoleId: 2 });

        expect(result).toMatchSnapshot();
    });

    it('setState - should be called when click Role Name button', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        const dataRow = shallow(<div>{instance._buildDataRow(roleList[0], gridData, 0, false)}</div>);

        dataRow.find("Button").at(0).simulate("click");
        expect(instance.setState).toHaveBeenCalledWith({ expandedRoleId: 1, isEdit: false });
    });

    it('setState - should be called when click Role Name button with collapsed', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        const dataRow = shallow(<div>{instance._buildDataRow(roleList[0], gridData, 1, false)}</div>);

        dataRow.find("Button").at(0).simulate("click");
        expect(instance.setState).toHaveBeenCalledWith({ expandedRoleId: 0, isEdit: false });
    });

    it('setState - should be called when click Edit button', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();

        instance.setState = jest.fn();

        const dataRow = shallow(<div>{instance._buildDataRow(roleList[1], gridData, 0, false)}</div>);

        dataRow.find(MyLink).at(0).simulate("click");
        expect(instance.setState).toHaveBeenCalledWith({ expandedRoleId: 2, isEdit: true });
    });

    it('_handleChange - should be called when click Read checkbox', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();

        instance._handleChange = jest.fn();

        const dataRow = shallow(<div>{instance._renderCells(gridData[5], true, true)}</div>);

        dataRow.find("input").at(0).simulate("change");
        expect(instance._handleChange).toHaveBeenCalledWith(2, 1, false, false);
    });

    it('_handleChange - should be called when click Host checkbox', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();

        instance._handleChange = jest.fn();

        const dataRow = shallow(<div>{instance._renderCells(gridData[5], true, true)}</div>);

        dataRow.find("input").at(1).simulate("change");
        expect(instance._handleChange).toHaveBeenCalledWith(2, 1, false, true);
    });

    it('_handleChangeChildrenOfItem - should return correct data', () => {
        const grid = [
            {
                AllParentIds: "24",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: false,
                ParentId: 24,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]

        const expectGrid = [
            {
                AllParentIds: "24",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 24,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();

        const data = instance._handleChangeChildrenOfItem(grid, { IsRead: true, IsHost: true }, 2, 24);

        expect(data).toEqual(expectGrid);
    });

    it('_handleChangeParentsOfItem - should return correct data with not change', () => {
        const grid = [
            {
                AllParentIds: "24",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 24,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]

        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();

        const data = instance._handleChangeParentsOfItem(grid, { IsRead: true, IsHost: true }, 2, 25, '24', false, false);

        expect(data).toEqual(grid);
    });

    it('_handleChangeParentsOfItem - should return correct data with updated data', () => {
        const grid = [
            {
                AllParentIds: "24",
                DisplayName: "isRecursive",
                FuncId: 24,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: false,
                ParentId: 24,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            },
            {
                AllParentIds: "24",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 24,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]

        const expectedGrid = [
            {
                AllParentIds: "24",
                DisplayName: "isRecursive",
                FuncId: 24,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 24,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            },
            {
                AllParentIds: "24",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 24,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]

        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();

        const data = instance._handleChangeParentsOfItem(grid, { IsRead: true, IsHost: true }, 2, 25, '24', true, true);

        expect(data).toEqual(expectedGrid);
    });

    it('_handleChange - should call setState 1', () => {
        const grid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: false,
                ParentId: 25,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]

        const expectGrid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 25,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ gridData: grid });
        instance.setState = jest.fn();

        instance._handleChange(2, 25, true, true);

        expect(instance.setState).toHaveBeenCalledWith({ gridData: expectGrid });
    });

    it('_handleChange - should call setState 2', () => {
        const grid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 25,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]

        const expectGrid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: true,
                ParentId: 25,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ gridData: grid });
        instance.setState = jest.fn();

        instance._handleChange(2, 25, false, true);

        expect(instance.setState).toHaveBeenCalledWith({ gridData: expectGrid });
    });

    it('_handleChange - should call setState 3', () => {
        const grid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 25,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]

        const expectGrid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: false,
                ParentId: 25,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ gridData: grid });
        instance.setState = jest.fn();

        instance._handleChange(2, 25, false, false);

        expect(instance.setState).toHaveBeenCalledWith({ gridData: expectGrid });
    });

    it('_handleChange - should call setState 4', () => {
        const grid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: false,
                ParentId: 25,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]

        const expectGrid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: true,
                ParentId: 25,
                Policy: 3,
                RoleId: 2,
                RoleName: "Exe. Settlement"
            }
        ]
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ gridData: grid });
        instance.setState = jest.fn();

        instance._handleChange(2, 25, true, false);

        expect(instance.setState).toHaveBeenCalledWith({ gridData: expectGrid });
    });

    it('_handleCancel - should call setState', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleCancel();

        expect(instance.setState).toHaveBeenCalledWith({ isEdit: false, gridData });
    });

    it('_handleCloseNotificationPopup - should call setState', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleSave = jest.fn();

        instance._handleCloseNotificationPopup(false);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: '' });
        expect(instance._handleSave).not.toHaveBeenCalled();
    });

    it('_handleCloseNotificationPopup - should call setState and _handleSave', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleSave = jest.fn();

        instance._handleCloseNotificationPopup(true);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: '' });
        expect(instance._handleSave).toHaveBeenCalled();
    });

    it('_handleSubmit - should call setState', () => {
        const result = shallow(<GeneralComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleSubmit(true);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: messages.AreYouSureUpdateRole, isConfirmPopup: true });
    });

    it('_handleSave - should save successfully', async () => {
        const props = Object.assign({}, defaultProps, { updatePermissionInfo: jest.fn(() => ({ IsSuccessful: true, Message: '' })) });
        const result = shallow(<GeneralComponent {...props} />);
        const instance = result.instance();
        instance.setState({ gridData });
        instance.setState = jest.fn();

        await instance._handleSave();

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: "Update the permission is successful!", isConfirmPopup: false, isEdit: false });
    });

    it('_handleSave - should save failed', async () => {
        const props = Object.assign({}, defaultProps, { updatePermissionInfo: jest.fn(() => ({ IsSuccessful: false, Message: 'failed!' })) });
        const result = shallow(<GeneralComponent {...props} />);
        const instance = result.instance();
        instance.setState({ gridData });
        instance.setState = jest.fn();

        await instance._handleSave();

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: 'failed!', isConfirmPopup: false });
    });

    it('_handleSave - should save has error', async () => {
        const props = Object.assign({}, defaultProps, { updatePermissionInfo: jest.fn() });
        const result = shallow(<GeneralComponent {...props} />);
        const instance = result.instance();
        instance.setState({ gridData });
        instance.setState = jest.fn();

        await instance._handleSave();

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: messages.AnError, isConfirmPopup: false });
    });
});
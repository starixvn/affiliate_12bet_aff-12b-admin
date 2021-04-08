import React from 'react';
import { PersonalPermissionTable } from '../PersonalPermissionTable';
import { shallow } from 'enzyme';

describe('admin info - components - PersonalPermissionTable', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

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

    const defaultProps = {
        isEdit: false,
        isCreateNew: false,
        isRoleFunctionDefault: false,
        permissionListByRole
    };

    it('render correctly', () => {
        const result = shallow(<PersonalPermissionTable {...defaultProps} />);
        result.instance().setState({ isSuccess: true, isChangingPassword: true });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with isEdit or isCreateNew', () => {
        const props = Object.assign({}, defaultProps, { isEdit: true });
        const result = shallow(<PersonalPermissionTable {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with permissionListByRole is empty', () => {
        const props = Object.assign({}, defaultProps, { permissionListByRole: [] });
        const result = shallow(<PersonalPermissionTable {...props} />);

        expect(result).toMatchSnapshot();
    });

    it("componentDidUpdate - should call _updatePermissionListByRole", () => {
        const props = Object.assign({}, defaultProps, { isRoleFunctionDefault: true });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();
        instance._updatePermissionListByRole = jest.fn();

        instance.componentDidUpdate({ permissionListByRole: [] });

        expect(instance._updatePermissionListByRole).toHaveBeenCalled();
    });

    it("_handleSetByDefault - should be called when click Set to Role's Default checkbox", () => {
        const props = Object.assign({}, defaultProps, { isCreateNew: true });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();
        instance._handleSetByDefault = jest.fn();

        result.find("input").at(0).simulate("change");

        expect(instance._handleSetByDefault).toHaveBeenCalledWith(true);
    });

    it('_handleChange - should be called when click Read checkbox', () => {
        const result = shallow(<PersonalPermissionTable {...defaultProps} />);
        const instance = result.instance();

        instance._handleChange = jest.fn();

        const dataRow = shallow(<div>{instance._renderCells(permissionListByRole[4], true, true)}</div>);

        dataRow.find("input").at(0).simulate("change");
        expect(instance._handleChange).toHaveBeenCalledWith(25, true, false);
    });

    it('_handleChange - should be called when click Host checkbox', () => {
        const result = shallow(<PersonalPermissionTable {...defaultProps} />);
        const instance = result.instance();

        instance._handleChange = jest.fn();

        const dataRow = shallow(<div>{instance._renderCells(permissionListByRole[4], true, true)}</div>);

        dataRow.find("input").at(1).simulate("change");
        expect(instance._handleChange).toHaveBeenCalledWith(25, true, true);
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
                RoleName: "Exe. Settlement"
            }
        ]
        const result = shallow(<PersonalPermissionTable {...defaultProps} />);
        const instance = result.instance();

        const data = instance._handleChangeChildrenOfItem(grid, { IsRead: true, IsHost: true }, 24);

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
                RoleName: "Exe. Settlement"
            }
        ]

        const result = shallow(<PersonalPermissionTable {...defaultProps} />);
        const instance = result.instance();

        const data = instance._handleChangeParentsOfItem(grid, { IsRead: true, IsHost: true }, 25, '24', false, false);

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
                RoleName: "Exe. Settlement"
            }
        ]

        const result = shallow(<PersonalPermissionTable {...defaultProps} />);
        const instance = result.instance();

        const data = instance._handleChangeParentsOfItem(grid, { IsRead: true, IsHost: true }, 25, '24', true, true);

        expect(data).toEqual(expectedGrid);
    });

    it('_handleChange - should call setPermissionListByRole 1', () => {
        const grid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: false,
                ParentId: 25,
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
                RoleName: "Exe. Settlement"
            }
        ]
        const props = Object.assign({}, defaultProps, { permissionListByRole: grid, setPermissionListByRole: jest.fn() });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();

        instance._handleChange(25, true, true);

        expect(props.setPermissionListByRole).toHaveBeenCalledWith(expectGrid);
    });

    it('_handleChange - should call setPermissionListByRole 2', () => {
        const grid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 25,
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
                RoleName: "Exe. Settlement"
            }
        ]
        const props = Object.assign({}, defaultProps, { permissionListByRole: grid, setPermissionListByRole: jest.fn() });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();

        instance._handleChange(25, false, true);

        expect(props.setPermissionListByRole).toHaveBeenCalledWith(expectGrid);
    });

    it('_handleChange - should call setPermissionListByRole 3', () => {
        const grid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: true,
                IsRead: true,
                ParentId: 25,
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
                RoleName: "Exe. Settlement"
            }
        ]
        const props = Object.assign({}, defaultProps, { permissionListByRole: grid, setPermissionListByRole: jest.fn() });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();

        instance._handleChange(25, false, false);

        expect(props.setPermissionListByRole).toHaveBeenCalledWith(expectGrid);
    });

    it('_handleChange - should call setPermissionListByRole 4', () => {
        const grid = [
            {
                AllParentIds: "25",
                DisplayName: "isRecursive",
                FuncId: 25,
                FuncName: "[Recursive]",
                IsHost: false,
                IsRead: false,
                ParentId: 25,
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
                RoleName: "Exe. Settlement"
            }
        ]
        const props = Object.assign({}, defaultProps, { permissionListByRole: grid, setPermissionListByRole: jest.fn() });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();

        instance._handleChange(25, true, false);

        expect(props.setPermissionListByRole).toHaveBeenCalledWith(expectGrid);
    });

    it("_handleSetByDefault - should only call handleSetRoleFunctionDefault", () => {
        const props = Object.assign({}, defaultProps, { handleSetRoleFunctionDefault: jest.fn() });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();
        instance._updatePermissionListByRole = jest.fn();

        instance._handleSetByDefault(false);

        expect(props.handleSetRoleFunctionDefault).toHaveBeenCalled();
        expect(instance._updatePermissionListByRole).not.toHaveBeenCalled();
    });

    it("_handleSetByDefault - should only call handleSetRoleFunctionDefault and _updatePermissionListByRole", () => {
        const props = Object.assign({}, defaultProps, { handleSetRoleFunctionDefault: jest.fn() });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();
        instance._updatePermissionListByRole = jest.fn();

        instance._handleSetByDefault(true);

        expect(props.handleSetRoleFunctionDefault).toHaveBeenCalled();
        expect(instance._updatePermissionListByRole).toHaveBeenCalled();
    });

    it("_updatePermissionListByRole - should only call handleSetRoleFunctionDefault and _updatePermissionListByRole", () => {
        const props = Object.assign({}, defaultProps, { setPermissionListByRole: jest.fn() });
        const result = shallow(<PersonalPermissionTable {...props} />);
        const instance = result.instance();

        instance._updatePermissionListByRole();

        expect(props.setPermissionListByRole).toHaveBeenCalledWith(permissionListByRole);
    });
});
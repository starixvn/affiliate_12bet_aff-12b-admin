import React from 'react';
import { EditPolicyForm } from '../EditPolicyForm';
import { shallow } from 'enzyme';

describe('administrator - common - components - EditPolicyForm ', () => {
    const gridData = [
        {
            FunctionDescription: "User Control",
            FunctionId: 21,
            GroupPolicy: 3,
            ParentId: 21,
            Policy: 3
        },
        {
            FunctionDescription: "Affiliate",
            FunctionId: 22,
            GroupPolicy: 1,
            ParentId: 22,
            Policy: 1
        },
        {
            FunctionDescription: "Payment",
            FunctionId: 23,
            GroupPolicy: 0,
            ParentId: 23,
            Policy: 0
        }
    ];

    const checkedPolicies = {
        21: {
            groupPermission: {HasRead: true, HasHost: true},
            individualPermission: {HasRead: true, HasHost: true}
        },
        22: {
            groupPermission: {HasRead: true, HasHost: false},
            individualPermission: {HasRead: true, HasHost: false}
        },
        23: {
            groupPermission: {HasRead: false, HasHost: false},
            individualPermission: {HasRead: false, HasHost: false}
        }
    }

    const defaultProps = {
        selectedId: 123,
        name: 'abc',
        isShowPopup: true,
        closePopup: jest.fn(),
        isAccountPolicy: true,
        isGetByIdList: false
    };

    it('render correctly with Role Policy', () => {
        const props = Object.assign({}, defaultProps, { isAccountPolicy: false });
        const result = shallow(<EditPolicyForm {...props} />);
    result.setState({ data: gridData, checkedPolicies});

        expect(result).toMatchSnapshot();
    });

    it('render correctly with Account Policy', () => {
        const result = shallow(<EditPolicyForm {...defaultProps} />);
        result.setState({ data: gridData, checkedPolicies});

        expect(result).toMatchSnapshot();
    });

    it('render correctly with Group Account Policy', () => {
        const props = Object.assign({}, defaultProps, { isGetByIdList: true });
        const result = shallow(<EditPolicyForm {...props} />);
        result.setState({ data: gridData, checkedPolicies});

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call _handleGetPolicyList', () => {
        const props = Object.assign({}, defaultProps, { selectedId: 234 });
        const result = shallow(<EditPolicyForm {...props} />);
        const instance = result.instance();
        instance._handleGetPolicyList = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(instance._handleGetPolicyList).toHaveBeenCalledWith(234, true, false);
    });

    it('_handleGetPolicyList - should call getRolePolicy and setState', async () => {
        const expectResult = {
            data: {
                FunctionDescription: "User Control",
                FunctionId: 21,
                ParentId: 21,
                Policy: 3
            },
            checkedPolicies: {
                groupPermission: {HasRead: true, HasHost: true},
                individualPermission: {}
            }
        }
        const props = Object.assign({}, defaultProps, { getRolePolicy: jest.fn(() => expectResult) });
        const result = shallow(<EditPolicyForm {...props} />);
        const instance = result.instance();

        result.setState = jest.fn();

        await instance._handleGetPolicyList(123, false, false);

        expect(props.getRolePolicy).toHaveBeenCalledWith({ departmentId: 123 });
        expect(result.setState).toHaveBeenCalledWith({ ...expectResult });
    });

    it('_handleGetPolicyList - should call getAccountPolicyList and setState', async () => {
        const expectResult = {
            data: {
                FunctionDescription: "User Control",
                FunctionId: 21,
                GroupPolicy: 3,
                ParentId: 21,
                Policy: 3
            },
            checkedPolicies: {
                groupPermission: {HasRead: true, HasHost: true},
                individualPermission: {HasRead: true, HasHost: true}
            }
        }
        const props = Object.assign({}, defaultProps, { getAccountPolicyList: jest.fn(() => expectResult) });
        const result = shallow(<EditPolicyForm {...props} />);
        const instance = result.instance();

        result.setState = jest.fn();

        await instance._handleGetPolicyList(123, true, false);

        expect(props.getAccountPolicyList).toHaveBeenCalledWith({ groupId: 123 });
        expect(result.setState).toHaveBeenCalledWith({ ...expectResult });
    });

    it('_handleGetPolicyList - should call getPolicyListByGroupIds and setState', async () => {
        const expectResult = {
            data: {
                FunctionDescription: "User Control",
                FunctionId: 21,
                GroupPolicy: 3,
                ParentId: 21,
                Policy: 3
            },
            checkedPolicies: {
                groupPermission: {HasRead: true, HasHost: true},
                individualPermission: {HasRead: true, HasHost: true}
            }
        }
        const props = Object.assign({}, defaultProps, { getPolicyListByGroupIds: jest.fn(() => expectResult) });
        const result = shallow(<EditPolicyForm {...props} />);
        const instance = result.instance();

        result.setState = jest.fn();

        await instance._handleGetPolicyList('123', true, true);

        expect(props.getPolicyListByGroupIds).toHaveBeenCalledWith({ groupIds: '123' });
        expect(result.setState).toHaveBeenCalledWith({ ...expectResult });
    });

    it('_handleChange - should call setState with HasHost checked in individual', () => {
        const event = {
            target: {
                name: 'HasHost',
                checked: true
            }
        };

        const result = shallow(<EditPolicyForm {...defaultProps} />);
        const instance = result.instance();
        result.setState({ checkedPolicies });
        result.setState = jest.fn();

        instance._handleChange(event, 21, false);

        expect(result.setState).toHaveBeenCalledWith({"checkedPolicies": {"21": {"groupPermission": {"HasHost": true, "HasRead": true}, "individualPermission": {"HasHost": true, "HasRead": true}}, "22": {"groupPermission": {"HasHost": false, "HasRead": true}, "individualPermission": {"HasHost": false, "HasRead": true}}, "23": {"groupPermission": {"HasHost": false, "HasRead": false}, "individualPermission": {"HasHost": false, "HasRead": false}}}});
    });

    it('_handleChange - should call setState with HasHost checked in Group', () => {
        const event = {
            target: {
                name: 'HasHost',
                checked: true
            }
        };

        const result = shallow(<EditPolicyForm {...defaultProps} />);
        const instance = result.instance();
        result.setState({ checkedPolicies });
        result.setState = jest.fn();

        instance._handleChange(event, 21, true);

        expect(result.setState).toHaveBeenCalledWith({"checkedPolicies": {"21": {"groupPermission": {"HasHost": true, "HasRead": true}, "individualPermission": {"HasHost": true, "HasRead": true}}, "22": {"groupPermission": {"HasHost": false, "HasRead": true}, "individualPermission": {"HasHost": false, "HasRead": true}}, "23": {"groupPermission": {"HasHost": false, "HasRead": false}, "individualPermission": {"HasHost": false, "HasRead": false}}}});
    });

    it('_handleChange - should call setState with HasRead in individual', () => {
        const event = {
            target: {
                name: 'HasRead',
                checked: false
            }
        };

        const result = shallow(<EditPolicyForm {...defaultProps} />);
        const instance = result.instance();
        result.setState({ checkedPolicies });
        result.setState = jest.fn();

        instance._handleChange(event, 21, false);

        expect(result.setState).toHaveBeenCalledWith({"checkedPolicies": {"21": {"groupPermission": {"HasHost": true, "HasRead": true}, "individualPermission": {"HasHost": true, "HasRead": false}}, "22": {"groupPermission": {"HasHost": false, "HasRead": true}, "individualPermission": {"HasHost": false, "HasRead": true}}, "23": {"groupPermission": {"HasHost": false, "HasRead": false}, "individualPermission": {"HasHost": false, "HasRead": false}}}});
    });

    it('_handleChange - should call setState with HasRead in Group', () => {
        const event = {
            target: {
                name: 'HasRead',
                checked: false
            }
        };

        const result = shallow(<EditPolicyForm {...defaultProps} />);
        const instance = result.instance();
        result.setState({ checkedPolicies });
        result.setState = jest.fn();

        instance._handleChange(event, 21, true);

        expect(result.setState).toHaveBeenCalledWith({"checkedPolicies": {"21": {"groupPermission": {"HasHost": true, "HasRead": false}, "individualPermission": {"HasHost": true, "HasRead": true}}, "22": {"groupPermission": {"HasHost": false, "HasRead": true}, "individualPermission": {"HasHost": false, "HasRead": true}}, "23": {"groupPermission": {"HasHost": false, "HasRead": false}, "individualPermission": {"HasHost": false, "HasRead": false}}}});
    });
})
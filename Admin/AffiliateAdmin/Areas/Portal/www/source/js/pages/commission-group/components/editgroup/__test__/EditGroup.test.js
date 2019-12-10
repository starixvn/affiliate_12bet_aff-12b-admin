import React from 'react';
import { EditGroup } from '../EditGroup';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';
jest.mock('../../../modules/constant');

describe('commission group - EditGroup', () => {
    const defaultProps = {
        selectedGroup: {
            BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
            Description: "",
            GroupId: 0,
            GroupName: "",
            OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4" }],
            QmTurnover: 100,
            TierSettingList: []
        },
        onClose: jest.fn(),
        onSave: jest.fn(),
        itemId: 1,
        pagingOption: defaultPagingOption,
        searchedGroupName: ''
    };
    it('componentDidMount - should call setSelectedCommGroup', () => {
        const mockGetSelectedCommGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { getSelectedCommGroup: mockGetSelectedCommGroup });

        const componentInstance = new EditGroup(props);

        componentInstance.componentDidMount();

        expect(props.getSelectedCommGroup).toHaveBeenCalledWith({ GroupId: 1 });

    });

    it('componentWillUnmount - should call getSelectedCommGroup', () => {
        const mockGetSelectedCommGroup = jest.fn();
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { getSelectedCommGroup: mockGetSelectedCommGroup, setSelectedGroup: mockSetSelectedGroup });

        const componentInstance = new EditGroup(props);

        componentInstance.componentWillUnmount();

        expect(props.setSelectedGroup).toHaveBeenCalledWith(null);

    });

    it('handleSave - should call setState when click button SAVE', () => {
        const mockGetSelectedCommGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { getSelectedCommGroup: mockGetSelectedCommGroup });
        const expectData = {
            isShowMessage: true,
            isSuccess: false,
            message: ["Group Name cannot be empty. Please check!", "Tier Setting must have at least 1 tier"]
        };
        const wrapper = shallow(<EditGroup {...props} />);
        wrapper.instance().setState = jest.fn();

        wrapper.find('button').simulate('click');

        expect(wrapper.instance().setState).toHaveBeenCalledWith(expectData);
    });

    it('handleSave - should call toggleLoader,  when click button SAVE with correct respone', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: "OK" }, status: 200 };
        const selectedGroupValid = {
            BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
            Description: "test",
            GroupId: 0,
            GroupName: "test",
            OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6,8" }],
            QmTurnover: 100,
            TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }]
        }
        const mockGetSelectedCommGroup = jest.fn();
        const mockToggleLoader = jest.fn();
        const mockUpdateCommissionGroup = jest.fn(() => expectedResponse);
        const mockGetGroupSettingList = jest.fn();

        const props = Object.assign({}, defaultProps, {
            getSelectedCommGroup: mockGetSelectedCommGroup, updateCommissionGroup: mockUpdateCommissionGroup,
            selectedGroup: selectedGroupValid, toggleLoader: mockToggleLoader, getGroupSettingList: mockGetGroupSettingList
        });
        window.setTimeout = jest.fn();
        const wrapper = shallow(<EditGroup {...props} />);
        wrapper.instance().setState = jest.fn();

        await wrapper.find('button').simulate('click');

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(window.setTimeout).toHaveBeenCalledTimes(1);
        expect(props.getGroupSettingList).toHaveBeenCalledWith({ PageIndex: 1, PageSize: 20, GroupName: '' });
        expect(wrapper.instance().setState).toHaveBeenCalledWith({ isShowMessage: true, isSuccess: true, message: ["Update Commission Group Successfully"] });
    });

    it('handleSave - should call toggleLoader,  when click button SAVE with correct respone but not success', async () => {
        const expectedResponse = { data: { IsSuccessful: false, Message: "fail" }, status: 200 };
        const selectedGroupValid = {
            BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
            Description: "test",
            GroupId: 0,
            GroupName: "test",
            OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6,8" }],
            QmTurnover: 100,
            TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }]
        }
        const mockGetSelectedCommGroup = jest.fn();
        const mockToggleLoader = jest.fn();
        const mockUpdateCommissionGroup = jest.fn(() => expectedResponse);
        const mockGetGroupSettingList = jest.fn();

        const props = Object.assign({}, defaultProps, {
            getSelectedCommGroup: mockGetSelectedCommGroup, updateCommissionGroup: mockUpdateCommissionGroup,
            selectedGroup: selectedGroupValid, toggleLoader: mockToggleLoader, getGroupSettingList: mockGetGroupSettingList
        });
        window.setTimeout = jest.fn();
        const wrapper = shallow(<EditGroup {...props} />);
        wrapper.instance().setState = jest.fn();

        await wrapper.find('button').simulate('click');

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(window.setTimeout).not.toHaveBeenCalled();
        expect(props.getGroupSettingList).not.toHaveBeenCalled();
        expect(wrapper.instance().setState).toHaveBeenCalledWith({ isShowMessage: true, isSuccess: false, message: ["fail"] });
    });

    it('handleSave - should call toggleLoader,  when click button SAVE with incorrect response', async () => {
        const expectedResponse = { status: 500 };

        const selectedGroupValid = {
            BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
            Description: "test",
            GroupId: 0,
            GroupName: "test",
            OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6,8" }],
            QmTurnover: 100,
            TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }]
        }
        const mockGetSelectedCommGroup = jest.fn();
        const mockToggleLoader = jest.fn();
        const mockUpdateCommissionGroup = jest.fn(() => expectedResponse);
        const mockGetGroupSettingList = jest.fn();

        const props = Object.assign({}, defaultProps, {
            getSelectedCommGroup: mockGetSelectedCommGroup, updateCommissionGroup: mockUpdateCommissionGroup,
            selectedGroup: selectedGroupValid, toggleLoader: mockToggleLoader, getGroupSettingList: mockGetGroupSettingList
        });
        window.setTimeout = jest.fn();
        const wrapper = shallow(<EditGroup {...props} />);
        wrapper.instance().setState = jest.fn();

        await wrapper.find('button').simulate('click');

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(window.setTimeout).not.toHaveBeenCalled();
        expect(props.getGroupSettingList).not.toHaveBeenCalled();
        expect(wrapper.instance().setState).not.toHaveBeenCalled();
    });

    it('should render correctly with isSuccess is true', () => {
        const mockGetSelectedCommGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { getSelectedCommGroup: mockGetSelectedCommGroup });
        const wrapper = shallow(<EditGroup {...props} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('should render correctly with isSuccess is false', () => {
        const mockGetSelectedCommGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { getSelectedCommGroup: mockGetSelectedCommGroup });
        const wrapper = shallow(<EditGroup {...props} />);
        wrapper.setState({ isSuccess: false });

        expect(wrapper).toMatchSnapshot();
    });
});
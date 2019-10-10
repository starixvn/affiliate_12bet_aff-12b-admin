import React from 'react';
import { AssignPrivateGroup } from '../AssignPrivateGroup';
import { shallow } from 'enzyme';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';
jest.mock('../../../modules/constant');

describe('commission group - assign private group', () => {
    const expectParam = {
        BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
        ExchangeCommissionShare: { DeductedPercentage: 0, DeductionId: 14, ProductIds: 19 },
        ExchangeWinLossShare: { DeductedPercentage: 0, DeductionId: 13, ProductIds: 19 },
        Description: "",
        GroupId: 0,
        GroupName: "",
        OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
        PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" }],
        QmTurnover: 100,
        TierSettingList: []
    };

    const selectedGroup = {
        BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
        Description: "",
        GroupId: 0,
        GroupName: "",
        OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
        PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" }],
        QmTurnover: 100,
        TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }]
    };

    const defaultProps = {
        selectedGroup: { GroupId: 102 },
        affInfo: {
            AffId: 927841,
            AffType: 4,
            Email: "walter.tran1@starixsoft.com",
            FirstName: "媒体册 获取以更新吧！",
            GroupId: 102,
            MailOption: false,
            PlayerUsername: "qatest01",
            PreferredLanguage: "hi-IN",
            UserName: "12BETAFF001"
        },
        deductionList: [
            { DeductionId: 10, DeductionName: "Platform Fee", DeductedPercentage: 0.17 },
            { DeductionId: 11, DeductionName: "Operational Fee", DeductedPercentage: 0.07 },
            { DeductionId: 12, DeductionName: "Banking Fee", DeductedPercentage: 0.02 },
            { DeductionId: 13, DeductionName: "Exchange Win Loss Share", DeductedPercentage: 0 },
            { DeductionId: 14, DeductionName: "Exchange Commission Share", DeductedPercentage: 0 }
        ],
        productList: [
            { ProductId: 1, ProductName: "Sportsbook" },
            { ProductId: 3, ProductName: "Playtech Casino" },
            { ProductId: 4, ProductName: "Number Game" },
            { ProductId: 5, ProductName: "Virtual Sports" },
            { ProductId: 6, ProductName: "Keno" }
        ]
    };

    it('componentDidMount - should call setState and setSelectedGroup', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        wrapper.setState = jest.fn();

        wrapper.instance().componentDidMount();

        expect(wrapper.setState).toHaveBeenCalledTimes(2);
        expect(wrapper.setState.mock.calls[0][0]).toEqual({ isShowLoader: true });
        expect(wrapper.setState.mock.calls[1][0]).toEqual({ isShowLoader: false });
        expect(props.setSelectedGroup).toHaveBeenCalledWith(expectParam);
    })

    it('componentDidMount - should call setState and getSelectedCommGroup', () => {
        const mockGetSelectedCommGroup = jest.fn();
        const affInfo = Object.assign({}, defaultProps.affInfo, { GroupId: 927841 })
        const props = Object.assign({}, defaultProps, { getSelectedCommGroup: mockGetSelectedCommGroup, affInfo });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        wrapper.setState = jest.fn();

        wrapper.instance().componentDidMount();

        expect(wrapper.setState).toHaveBeenCalledTimes(2);
        expect(wrapper.setState.mock.calls[0][0]).toEqual({ isShowLoader: true });
        expect(wrapper.setState.mock.calls[1][0]).toEqual({ isShowLoader: false });
        expect(props.getSelectedCommGroup).toHaveBeenCalledWith({ GroupId: 927841 });
    })

    it('componentWillUnmount - should call setSelectedGroup', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        wrapper.instance().componentWillUnmount();

        expect(props.setSelectedGroup).toHaveBeenCalledWith(null);
    })

    it('handleSave - should call setSate with isValid is false', async () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);
        wrapper.setState = jest.fn((obj, fn) => fn && fn());
        window.setTimeout = jest.fn((fn, time) => fn());
        wrapper.instance()._validate = jest.fn(() => ({ isValid: false, messageList: 'failed!' }));

        await wrapper.find('button').simulate('click');

        expect(wrapper.setState).toHaveBeenCalledTimes(2);
        expect(wrapper.setState.mock.calls[0][0]).toEqual({ isShowMessage: true, isSuccess: false, message: 'failed!' });
        expect(wrapper.setState.mock.calls[0][1]).toEqual(expect.any(Function));

        expect(wrapper.setState.mock.calls[1][0]).toEqual({ isShowMessage: false });
    })

    it('handleSave - should call setSate,  window.scrollTo, window.setTimeout, parent.postMessage with isValid and IsSuccessful are true', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: "OK" }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);
        wrapper.setState = jest.fn((obj, fn) => fn && fn());
        window.setTimeout = jest.fn((fn, time) => fn());
        window.scrollTo = jest.fn();
        parent.postMessage = jest.fn();

        wrapper.instance()._validate = jest.fn(() => ({ isValid: true, messageList: 'OK' }));

        await wrapper.find('button').simulate('click');

        expect(wrapper.setState).toHaveBeenCalledTimes(3);
        expect(wrapper.setState.mock.calls[0][0]).toEqual({ isShowLoader: true });
        expect(wrapper.setState.mock.calls[1][0]).toEqual({ isShowMessage: true, isSuccess: true, message: ["Update Commission Group Successfully"], isShowLoader: false });
        expect(window.scrollTo).toHaveBeenCalledWith({ top: 0, behavior: "smooth" });
        expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
        expect(parent.postMessage).toHaveBeenCalledWith("updatePrivateGroupSuccessfully", "http://localhost");
    })

    it('handleSave - should call setSate,  window.scrollTo with isValid, GroupId is zero', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: "OK" }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const mockSetSelectedGroup = jest.fn();
        const affInfo = Object.assign({}, defaultProps.affInfo, { GroupId: 0 });
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selectedGroup, affInfo: affInfo });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);
        wrapper.setState = jest.fn();
        window.scrollTo = jest.fn();

        wrapper.instance()._validate = jest.fn(() => ({ isValid: true, messageList: 'OK' }));

        await wrapper.find('button').simulate('click');

        expect(wrapper.setState).toHaveBeenCalledTimes(2);
        expect(wrapper.setState.mock.calls[0][0]).toEqual({ isShowLoader: true });
        expect(wrapper.setState.mock.calls[1][0]).toEqual({ isShowMessage: true, isSuccess: true, message: ["Assign affiliate to commission group successfully"], isShowLoader: false });
        expect(window.scrollTo).toHaveBeenCalledWith({ top: 0, behavior: "smooth" })
    })

    it('handleSave - should call setSate,  window.scrollTo, window.setTimeout and not call parent.postMessage with isValid is true and IsSuccessful is false', async () => {
        const expectedResponse = { data: { IsSuccessful: false, Message: "failed!" }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);
        wrapper.setState = jest.fn((obj, fn) => fn && fn());
        window.setTimeout = jest.fn((fn, time) => fn());
        window.scrollTo = jest.fn();
        parent.postMessage = jest.fn();

        wrapper.instance()._validate = jest.fn(() => ({ isValid: true, messageList: 'OK' }));

        await wrapper.find('button').simulate('click');

        expect(wrapper.setState).toHaveBeenCalledTimes(3);
        expect(wrapper.setState.mock.calls[0][0]).toEqual({ isShowLoader: true });
        expect(wrapper.setState.mock.calls[1][0]).toEqual({ isShowMessage: true, isSuccess: false, message: ["failed!"], isShowLoader: false });
        expect(window.scrollTo).toHaveBeenCalledWith({ top: 0, behavior: "smooth" });
        expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
        expect(parent.postMessage).not.toHaveBeenCalled();
    })

    it('handleSave - should call setSate with respone erro', async () => {
        const expectedResponse = { data: { IsSuccessful: false, Message: "failed!" }, status: 500 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);
        wrapper.setState = jest.fn();
        window.scrollTo = jest.fn();

        wrapper.instance()._validate = jest.fn(() => ({ isValid: true, messageList: 'OK' }));

        await wrapper.find('button').simulate('click');

        expect(wrapper.setState).toHaveBeenCalledTimes(1);
        expect(wrapper.setState.mock.calls[0][0]).toEqual({ isShowLoader: true });
        expect(window.scrollTo).not.toHaveBeenCalled()
    })

    it('_validate', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        const result = wrapper.instance()._validate();

        expect(result).toEqual({ isValid: true, messageList: [] });
    })

    it('_validate - QmTurnover is NaN', () => {
        const selected_Group = Object.assign({}, selectedGroup, { QmTurnover: 'abc' })
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selected_Group });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        const result = wrapper.instance()._validate();

        expect(result).toEqual({ isValid: false, messageList: ["QM Turnover value is required"] });
    })

    it('_validate - TierSettingList is not MaxAmount', () => {
        const selected_Group = Object.assign({}, selectedGroup, { TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100, MinQm: 0, MaxQm: 10, Commission: 0 }] })
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selected_Group });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        const result = wrapper.instance()._validate();

        expect(result).toEqual({ isValid: false, messageList: ["Last Tier Setting must have maximum value=∞"] });
    })

    it('_validate - TierSettingList has MinAmount > MaxAmount', () => {
        const selected_Group = Object.assign({}, selectedGroup, { TierSettingList: [{ TierId: 1, MinAmount: 1000000000000000, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }] })
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selected_Group });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        const result = wrapper.instance()._validate();

        expect(result).toEqual({ isValid: false, messageList: ["Max Amount must be greater than or equal to Min Amount"] });
    })

    it('_validate - TierSettingList has MinQm > MaxQm', () => {
        const selected_Group = Object.assign({}, selectedGroup, { TierSettingList: [{ TierId: 1, MinAmount: 10, MaxAmount: 100000000000000, MinQm: 10000000000, MaxQm: 1000000000, Commission: 0 }] })
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selected_Group });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        const result = wrapper.instance()._validate();

        expect(result).toEqual({ isValid: false, messageList: ["Max QM must be greater than or equal to Min QM"] });
    })

    it('_validate - PlatformFeeList has ProductId is null', () => {
        const selected_Group = Object.assign({}, selectedGroup, { PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "" }] })
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selected_Group });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);

        const result = wrapper.instance()._validate();

        expect(result).toEqual({ isValid: false, messageList: ["At least 1 product must be selected per group"] });
    })

    it('should render correctly', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);
        expect(wrapper).toMatchSnapshot();
    })

    it('should render correctly with isSuccess is false', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<AssignPrivateGroup {...props} />);
        wrapper.setState({ isSuccess: false });
        expect(wrapper).toMatchSnapshot();
    })
})
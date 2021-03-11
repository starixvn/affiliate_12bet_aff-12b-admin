import React from 'react';
import { LanguageComponent } from '../LanguageComponent';
import { shallow } from 'enzyme';
import { messages } from '../../libs/constants';

describe('role permission - components - LanguageComponent', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const item1 = {
        RoleId: 1,
        RoleName: "Management",
        'en-US': true,
        'vi-VN': true,
    };

    const item2 = {
        RoleId: 2,
        RoleName: "Exe. Settlement",
        'en-US': true,
        'vi-VN': false,
    };

    const gridData = [item1, item2];

    const roleList = [
        { RoleId: 1, RoleName: "Management" },
        { RoleId: 2, RoleName: "Exe. Settlement" }
    ];

    const marketIdList = {
        "en-US": "en-US",
        "vi-VN": "vi-VN"
    };

    const marketList = [
        { RoleId: 1, MarketId: "en-US", MarketName: "English", HasPermission: true },
        { RoleId: 1, MarketId: "vi-VN", MarketName: "Vietnamese", HasPermission: true },
        { RoleId: 2, MarketId: "en-US", MarketName: "English", HasPermission: true },
        { RoleId: 2, MarketId: "vi-VN", MarketName: "Vietnamese", HasPermission: false },
        { RoleId: 2, MarketId: "th-TH", MarketName: "Thai", HasPermission: false }
    ];

    const defaultProps = {
        gridData,
        roleList,
        marketIdList,
        marketList,
        toggleLoader: jest.fn(),
        setGridData: jest.fn(),
        getMarketListByRole: jest.fn(() => marketList),
        hasWriteAdminRoleClaim: true
    }

    it('render correctly', () => {
        const result = shallow(<LanguageComponent {...defaultProps} />);
        result.setState({ isEdit: true, messagePopup: 'test', gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not write permission', () => {
        const props = Object.assign({}, defaultProps, { hasWriteAdminRoleClaim: false });
        const result = shallow(<LanguageComponent {...props} />);
        result.setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] });
        const result = shallow(<LanguageComponent {...props} />);
        result.setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has marketList is empty', () => {
        const props = Object.assign({}, defaultProps, { marketList: [], marketIdList: null });
        const result = shallow(<LanguageComponent {...props} />);
        result.setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('setState - should be called when click edit button', () => {
        const result = shallow(<LanguageComponent {...defaultProps} />);
        result.setState({ gridData });
        result.setState = jest.fn();

        result.find("Button").at(0).simulate("click");
        expect(result.setState).toHaveBeenCalledWith({ isEdit: true });
    });

    it('_handleChange - should be called when click checkbox', () => {
        const result = shallow(<LanguageComponent {...defaultProps} />);
        const instance = result.instance();

        instance._handleChange = jest.fn();

        const dataRow = shallow(<div>{instance._renderColumnBody(gridData[0], marketList, true)}</div>);

        dataRow.find("input").at(0).simulate("change");
        expect(instance._handleChange).toHaveBeenCalledWith(1, "en-US", true);
    });

    it('_handleChange - should call setState', () => {
        const expectNewItem = Object.assign({}, item1, { "en-US": false });
        const result = shallow(<LanguageComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ gridData });
        instance.setState = jest.fn();

        instance._handleChange(1, "en-US", true);

        expect(instance.setState).toHaveBeenCalledWith({ gridData: [expectNewItem, item2] });
    });

    it('_handleCancel - should call setState', () => {
        const result = shallow(<LanguageComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleCancel();

        expect(instance.setState).toHaveBeenCalledWith({ isEdit: false, gridData });
    });

    it('handleConvertData - should return correct data', () => {
        const result = shallow(<LanguageComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ gridData });

        const data = instance.handleConvertData();

        expect(data).toEqual([
            { RoleId: 1, MarketId: "en-US", HasPermission: true },
            { RoleId: 1, MarketId: "vi-VN", HasPermission: true },
            { RoleId: 1, MarketId: "en-US", HasPermission: true },
            { RoleId: 1, MarketId: "vi-VN", HasPermission: true },
            { RoleId: 1, MarketId: "th-TH", HasPermission: undefined },
            { RoleId: 2, MarketId: "en-US", HasPermission: true },
            { RoleId: 2, MarketId: "vi-VN", HasPermission: false },
            { RoleId: 2, MarketId: "en-US", HasPermission: true },
            { RoleId: 2, MarketId: "vi-VN", HasPermission: false },
            { RoleId: 2, MarketId: "th-TH", HasPermission: undefined }
        ]);
    });

    it('_handleCloseNotificationPopup - should call setState', () => {
        const result = shallow(<LanguageComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleSave = jest.fn();

        instance._handleCloseNotificationPopup(false);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: '' });
        expect(instance._handleSave).not.toHaveBeenCalled();
    });

    it('_handleCloseNotificationPopup - should call setState and _handleSave', () => {
        const result = shallow(<LanguageComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleSave = jest.fn();

        instance._handleCloseNotificationPopup(true);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: '' });
        expect(instance._handleSave).toHaveBeenCalled();
    });

    it('_handleSubmit - should call setState', () => {
        const result = shallow(<LanguageComponent {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleSubmit(true);

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: messages.AreYouSureUpdateLanguages, isConfirmPopup: true });
    });

    it('_handleSave - should save successfully', async () => {
        const props = Object.assign({}, defaultProps, { updateMarketInfoList: jest.fn(() => ({ IsSuccessful: true, Message: '' })) });
        const result = shallow(<LanguageComponent {...props} />);
        const instance = result.instance();
        instance.setState({ gridData });
        instance.setState = jest.fn();

        await instance._handleSave();

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: "Update markets for roles are successful!", isConfirmPopup: false, isEdit: false });
    });

    it('_handleSave - should save failed', async () => {
        const props = Object.assign({}, defaultProps, { updateMarketInfoList: jest.fn(() => ({ IsSuccessful: false, Message: 'failed!' })) });
        const result = shallow(<LanguageComponent {...props} />);
        const instance = result.instance();
        instance.setState({ gridData });
        instance.setState = jest.fn();

        await instance._handleSave();

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: 'failed!', isConfirmPopup: false });
    });

    it('_handleSave - should save has error', async () => {
        const props = Object.assign({}, defaultProps, { updateMarketInfoList: jest.fn() });
        const result = shallow(<LanguageComponent {...props} />);
        const instance = result.instance();
        instance.setState({ gridData });
        instance.setState = jest.fn();

        await instance._handleSave();

        expect(instance.setState).toHaveBeenCalledWith({ messagePopup: messages.AnError, isConfirmPopup: false });
    });
});
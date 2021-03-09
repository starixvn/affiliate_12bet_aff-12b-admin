import React from 'react';
import { LanguageComponent } from '../LanguageComponent';
import { shallow } from 'enzyme';

describe('role permission - components - LanguageComponent', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const gridData = [{
        RoleId: 1,
        RoleName: "Management",
        'en-US': true,
        'vi-VN': true,
    },
    {
        RoleId: 2,
        RoleName: "Exe. Settlement",
        'en-US': true,
        'vi-VN': false,
    }];

    const roleList = [
        { RoleId: 1, RoleName: "Management" },
        { RoleId: 2, RoleName: "Exe. Settlement" }
    ];

    const marketIdList = {
        "en-US": "en-US",
        "vi-VN": "vi-VN"
    };

    const marketList = [
        { RoleId: 0, MarketId: "en-US", MarketName: "English", HasPermission: true },
        { RoleId: 0, MarketId: "vi-VN", MarketName: "Vietnamese", HasPermission: false },
        { RoleId: 0, MarketId: "th-TH", MarketName: "Thai", HasPermission: false }
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

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not write permission', () => {
        const props = Object.assign({}, defaultProps, { hasWriteAdminRoleClaim: false });
        const result = shallow(<LanguageComponent {...props} />);

        expect(result).toMatchSnapshot();
    });
});
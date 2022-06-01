import React from 'react';
import { Additional } from '../Additional';
import { shallow } from 'enzyme';

describe('user account - components - sections - Additional', () => {
    const userAccountInfo = {
        AffId: 1271808,
        AffName: "12BETAFF226",
        FirstName: "Hinh",
        LastName: "Lê Văn",
        Gender: false,
        Email: "hubert.duong_1@nexcel.vn",
        Phone: "abc",
        Address: "lê thị hồng",
        City: "Hồ Chí Minh",
        State: "700000",
        Postcode: "700000",
        Country: "Viet Nam",
        SiteUrl: "https://aff-12bet.nexdev.net/12BetUserAsianNew/UserPanel/Main/#MyAccount",
        MoreSiteUrls: "www.sportsbookie.com;www.gzyyy.com;www.123.com",
        NoSiteDescription: "",
        PreferredPayment: "Player",
        PlayerUsername: "bettingexp",
        NetellerAccountId: "1",
        MoneybookersEmail: "duong@gmail.com",
        BankName: "Bank Name",
        BankAddress: "Bank Address",
        BankCode: "Bank ABA/Swift/Sort Code",
        BankAccountName: "Bank Account Name",
        BankAccountNumber: "Bank Account Number",
        Question: "What?",
        Answer: "What?",
        MailOption: false,
        SecurityQuestions: [{ "Key": 11, "Value": "Name of the worst kid who bullied you?" }, { "Key": 12, "Value": "Name of your childhood bestfriend?" }],
        ContactAppList: [{Name: "MSN/YM/QQ", Value: "@123 Lê Văn", Order: 0}]
    };
    const defaultProps = {
        userAccountInfo,
        claimsPrincipal: { hasReadClaim: jest.fn(() => true), hasWriteClaim: jest.fn(() => true) },
        hasWriteEditAffClaim: true,
        setUserAccountInfo: jest.fn()
    }

    it('render correctly', () => {
        const result = shallow(<Additional {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly when not read claim', () => {
        const newUserAccountInfo = Object.assign({}, userAccountInfo, { SecurityQuestions: null });
        const props = Object.assign({}, defaultProps, { userAccountInfo: newUserAccountInfo, hasWriteEditAffClaim: false, claimsPrincipal: { hasReadClaim: jest.fn(() => false), hasWriteClaim: jest.fn(() => false) } });
        const result = shallow(<Additional {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('handleChange - should call setUserAccountInfo', () => {
        const result = shallow(<Additional {...defaultProps} />);

        result.instance().handleChange({ target: { name: 'address', value: '123' }});

        expect(defaultProps.setUserAccountInfo).toHaveBeenCalledWith({ ...userAccountInfo, Address: '123'});
    });
});
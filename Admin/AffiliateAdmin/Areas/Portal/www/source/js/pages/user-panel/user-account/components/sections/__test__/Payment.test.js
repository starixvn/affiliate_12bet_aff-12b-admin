import React from 'react';
import { Payment } from '../Payment';
import { shallow } from 'enzyme';

describe('user account - components - sections - Payment', () => {
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
        ContactAppList: [{Name: "MSN/YM/QQ", Value: "@123 Lê Văn", Order: 0}]
    };
    const defaultProps = {
        userAccountInfo,
        hasWriteEditAffClaim: true,
        hasReadClaim: true,
        hasWriteClaim: true,
        setUserAccountInfo: jest.fn()
    }

    it('render correctly', () => {
        const result = shallow(<Payment {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly when not has read claim', () => {
        const props = Object.assign({}, defaultProps, { hasReadClaim: false, hasWriteClaim: false });
        const result = shallow(<Payment {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('handleChangePaymentMethod - should call setUserAccountInfo', () => {
        const result = shallow(<Payment {...defaultProps} />);

        result.instance().handleChangePaymentMethod({ target: { value: 'Neteller' }});

        expect(defaultProps.setUserAccountInfo).toHaveBeenCalledWith({ ...userAccountInfo, PreferredPayment: 'Neteller'});
    });

    it('handleChange - should call setUserAccountInfo', () => {
        const result = shallow(<Payment {...defaultProps} />);

        result.instance().handleChange({ target: { name: 'bankName', value: '123' }});

        expect(defaultProps.setUserAccountInfo).toHaveBeenCalledWith({ ...userAccountInfo, BankName: '123'});
    });
});
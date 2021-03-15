import React from 'react';
import ApprovedStatusInfo from '../../ApprovedStatusInfo';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';

const mockStore = configureMockStore();

describe('left panel - ApprovedStatusInfo - connect', () => {
    let wrapper;
    let store;
    const affiliateInfo = { AffName: '12BETAFF226' };

    beforeEach(() => {
        const initialState = {
            leftPanel: {
                affiliateInfo
            },
            affCommonInfo: { affName: '12BETAFF226' },
            claimsPrincipal: { hasWriteClaim: jest.fn(() => true), hasReadClaim: jest.fn(() => true) }
        };
        store = mockStore(initialState);
        wrapper = shallow(<ApprovedStatusInfo store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().affName).toEqual('12BETAFF226');
        expect(wrapper.props().affiliateInfo).toEqual(affiliateInfo);
        expect(wrapper.props().isPwdChangeable).toBeTruthy();
        expect(wrapper.props().isShowMemberLstLink).toBeTruthy();
    });
})
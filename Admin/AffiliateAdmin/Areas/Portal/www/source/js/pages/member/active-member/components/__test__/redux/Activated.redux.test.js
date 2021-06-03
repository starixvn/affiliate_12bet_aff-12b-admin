import React from 'react';
import Activated from '../../Activated';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';

const mockStore = configureMockStore();

describe('Active member list - Activated - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            claimsPrincipal: { hasReadClaim: jest.fn(() => false) }
        };

        store = mockStore(initialState);
        wrapper = shallow(<Activated store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isShowMemberFullname).toBeFalsy();
    });
})
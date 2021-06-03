import React from 'react';
import Unactivated from '../../Unactivated';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SHOULD_RESET_SCROLL_POSITION } from '../../../actions/types';

const mockStore = configureMockStore();

describe('active member list - Unactivated - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            claimsPrincipal: { hasReadClaim: jest.fn(() => false) },
            shouldResetScrollPosition: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<Unactivated store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isShowMemberFullname).toBeFalsy();
        expect(wrapper.props().shouldResetScrollPosition).toBeTruthy();
    });

    it('should call correct action', () => {
        wrapper.props().setShouldResetScrollPosition(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SHOULD_RESET_SCROLL_POSITION, data: false }]);
    });
})
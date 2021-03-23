import React from 'react';
import AppContainer from '../../AppContainer';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SHOULD_RESET_SCROLL_POSITION } from '../../../actions/types';

const mockStore = configureMockStore();

describe('banner list - components - AppContainer - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            isShowLoader: true,
            claimsPrincipal: { hasWriteClaim: jest.fn(() => true) }
        };
        store = mockStore(initialState);
        wrapper = shallow(<AppContainer store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isShowLoader).toBeTruthy();
        expect(wrapper.props().isModifiable).toBeTruthy();
    });

    it('should call correct action', () => {
        wrapper.props().setShouldResetScrollPosition(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SHOULD_RESET_SCROLL_POSITION, data: false }]);
    });
})
import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SHOULD_RESET_SCROLL_POSITION } from '../../../actions/types';

const mockStore = configureMockStore();

describe('news letter - components - Content - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            claimsPrincipal: { hasWriteClaim: jest.fn(() => true ) }
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isModifiable).toBeTruthy();
    });

    it('should call correct action', () => {
        wrapper.props().setShouldResetScrollPosition(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SHOULD_RESET_SCROLL_POSITION, data: false }]);
    });
})
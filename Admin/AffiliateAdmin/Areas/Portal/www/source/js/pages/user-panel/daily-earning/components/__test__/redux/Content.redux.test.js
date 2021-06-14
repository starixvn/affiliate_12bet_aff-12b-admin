import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';

const mockStore = configureMockStore();

describe('daily earning - components - Content - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            claimsPrincipal: { hasReadClaim: jest.fn(() => true) },
            dailyEarning: { param: null }
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isDisplayTurnOver).toTruthy;
    });

    it('should call correct action', () => {
        wrapper.props().toggleLoaderUserPanel(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: "TOGGLE_LOADER_USER_PANEL", data: false }]);
    });
})
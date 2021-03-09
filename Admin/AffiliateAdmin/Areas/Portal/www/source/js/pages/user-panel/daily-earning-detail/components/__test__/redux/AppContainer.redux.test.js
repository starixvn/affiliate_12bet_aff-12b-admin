import React from 'react';
import AppContainer from '../../AppContainer';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';

const mockStore = configureMockStore();

describe('user panel - daily earning detail - AppContainer - connect', () => {
    let wrapper;
    let store;
    const param = { AffName: '12BETAFF226' };

    beforeEach(() => {
        const initialState = {
            dailyEarningDetail: {
                param
            }
        };
        store = mockStore(initialState);
        wrapper = shallow(<AppContainer store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().param).toEqual(param);
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedUserPanel('test');

        const actions = store.getActions();
        expect(actions).toEqual([{ type: 'USER_PANEL/SET_SELECTED_USERPANEL', data: 'test' }]);
    });
})
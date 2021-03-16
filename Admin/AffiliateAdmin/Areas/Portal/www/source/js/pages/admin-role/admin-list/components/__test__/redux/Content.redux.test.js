import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SHOULD_RESET_SCROLL_POSITION } from '../../../actions/types';

const mockStore = configureMockStore();

describe('admin list - components - Content - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            adminList: {
                isBack: false,
                param: {},
                checkedUsers: '1,2,3',
                shouldResetScrollPosition: true,
                gridData: []
            },
            hasWriteAdminRoleClaim: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isBack).toBeFalsy();
        expect(wrapper.props().param).toEqual({});
        expect(wrapper.props().checkedUsers).toEqual('1,2,3');
        expect(wrapper.props().gridData).toEqual([]);
        expect(wrapper.props().shouldResetScrollPosition).toBeTruthy();
        expect(wrapper.props().hasWriteAdminRoleClaim).toBeTruthy();
    });

    it('should call correct action', () => {
        wrapper.props().setShouldResetScrollPosition(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SHOULD_RESET_SCROLL_POSITION, data: false }]);
    });
})
import React from 'react';
import { mount, shallow } from 'enzyme';
import thunk from 'redux-thunk';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';
import ModifyNote from '../../ModifyNote';
import InsertUpdateForm from '../../../../common/components/InsertUpdateForm';

jest.mock('../../../../common/components/InsertUpdateForm', () => 'mocked-InsertUpdateForm');

const middlewares = [thunk];
const mockStore = configureStore(middlewares);

const initialState = {
    affCommonInfo: {
        affId: 1
    },
    note: {
        pagingOption: {
            pageIndex: 1,
            pageSize: 10
        },
        noteInfo: {},
        isShowPopup: true
    }
};

describe('aff-note - ModifyNote - connect', () => {
    it('renders without crashing', () => {
        const store = mockStore(initialState);
        const wrapper = shallow(<ModifyNote store={store} />);
        expect(wrapper).toMatchSnapshot();
    });

    it('should render the container component', () => {
        const store = mockStore(initialState);

        const wrapper = mount(
            <Provider store={store}>
                <ModifyNote />
            </Provider>
        );

        const container = wrapper.find(ModifyNote);
        const insertUpdate = container.find(InsertUpdateForm);

        expect(insertUpdate.exists()).toBe(true);
        expect(insertUpdate.props().affId).toBe(1);
        expect(insertUpdate.props().pagingOption).toBe(initialState.note.pagingOption);
        expect(insertUpdate.props().noteInfo).toBe(initialState.note.noteInfo);
        expect(insertUpdate.props().isShowPopup).toBe(true);
    });
});

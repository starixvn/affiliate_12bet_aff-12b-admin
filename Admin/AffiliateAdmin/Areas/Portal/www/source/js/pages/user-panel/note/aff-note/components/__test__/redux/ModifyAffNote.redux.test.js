import React from 'react';
import { mount, shallow } from 'enzyme';
import thunk from 'redux-thunk';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';
import ModifyAffNote from '../../ModifyAffNote';
import InsertUpdateForm from '../../../../common/components/InsertUpdateForm';

jest.mock('../../../../common/components/InsertUpdateForm', () => 'mocked-InsertUpdateForm');

const middlewares = [thunk];
const mockStore = configureStore(middlewares);

const initialState = {
    affCommonInfo: {
        affId: 1
    },
    note: {
        affNote: {
            pagingOption: {
                pageIndex: 1,
                pageSize: 10
            },
            affNoteInfo: {},
            isShowPopup: true
        }
    }
};

describe('aff-note - ModifyAffNote - connect', () => {
    it('renders without crashing', () => {
        const store = mockStore(initialState);
        const wrapper = shallow(<ModifyAffNote store={store} />);
        expect(wrapper).toMatchSnapshot();
    });

    it('should render the container component', () => {
        const store = mockStore(initialState);

        const wrapper = mount(
            <Provider store={store}>
                <ModifyAffNote />
            </Provider>
        );

        const container = wrapper.find(ModifyAffNote);
        const insertUpdate = container.find(InsertUpdateForm);

        expect(insertUpdate.exists()).toBe(true);
        expect(insertUpdate.props().affId).toBe(1);
        expect(insertUpdate.props().pagingOption).toBe(initialState.note.affNote.pagingOption);
        expect(insertUpdate.props().noteInfo).toBe(initialState.note.affNote.affNoteInfo);
        expect(insertUpdate.props().isShowPopup).toBe(true);
    });
});

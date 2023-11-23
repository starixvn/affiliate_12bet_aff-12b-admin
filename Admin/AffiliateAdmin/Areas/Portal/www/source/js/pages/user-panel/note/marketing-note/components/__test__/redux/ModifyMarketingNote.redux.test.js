import React from 'react';
import { mount, shallow } from 'enzyme';
import thunk from 'redux-thunk';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';
import ModifyMarketingNote from '../../ModifyMarketingNote';
import InsertUpdateForm from '../../../../common/components/InsertUpdateForm';

jest.mock('../../../../common/components/InsertUpdateForm', () => 'mocked-InsertUpdateForm');

const middlewares = [thunk];
const mockStore = configureStore(middlewares);

const initialState = {
    affCommonInfo: {
        affId: 1
    },
    note: {
        marketingNote: {
            pagingOption: {
                pageIndex: 1,
                pageSize: 10
            },
            marketingNoteInfo: {},
            isShowPopup: true
        }
    }
};

describe('marketing-note - ModifyMarketingNote - connect', () => {
    it('renders without crashing', () => {
        const store = mockStore(initialState);
        const wrapper = shallow(<ModifyMarketingNote store={store} />);
        expect(wrapper).toMatchSnapshot();
    });

    it('should render the container component', () => {
        const store = mockStore(initialState);

        const wrapper = mount(
            <Provider store={store}>
                <ModifyMarketingNote />
            </Provider>
        );

        const container = wrapper.find(ModifyMarketingNote);
        const insertUpdate = container.find(InsertUpdateForm);

        expect(insertUpdate.exists()).toBe(true);
        expect(insertUpdate.props().affId).toBe(1);
        expect(insertUpdate.props().pagingOption).toBe(initialState.note.marketingNote.pagingOption);
        expect(insertUpdate.props().noteInfo).toBe(initialState.note.marketingNote.marketingNoteInfo);
        expect(insertUpdate.props().isShowPopup).toBe(true);
    });
});

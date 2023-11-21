import React from 'react';
import { mount, shallow } from 'enzyme';
import thunk from 'redux-thunk';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';
import NoteContent from '../../NoteContent';
import Content from '../../../../common/components/Content';

jest.mock('../../../../common/components/Content', () => 'mocked-Content');

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
        noteList: [],
        noteInfo: {}
    },
    claimsPrincipal: {
        hasWriteClaim: () => true
    }
};

describe('aff-note - NoteContent - connect', () => {
    it('renders without crashing', () => {
        const store = mockStore(initialState);
        const wrapper = shallow(<NoteContent store={store} />);
        expect(wrapper).toMatchSnapshot();
    });

    it('should render the container component', () => {
        const store = mockStore(initialState);

        const wrapper = mount(
            <Provider store={store}>
                <NoteContent />
            </Provider>
        );

        const container = wrapper.find(NoteContent);
        const content = container.find(Content);

        expect(content.exists()).toBe(true);
        expect(content.props().affId).toBe(1);
        expect(content.props().pagingOption).toBe(initialState.note.pagingOption);
        expect(content.props().noteList).toBe(initialState.note.noteList);
        expect(content.props().noteInfo).toBe(initialState.note.noteInfo);
        expect(content.props().isEditNote).toBe(true);
    });
});
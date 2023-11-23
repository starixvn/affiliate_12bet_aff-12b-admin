import React from 'react';
import { mount, shallow } from 'enzyme';
import thunk from 'redux-thunk';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';
import AffNoteContent from '../../AffNoteContent';
import Content from '../../../../common/components/Content';

jest.mock('../../../../common/components/Content', () => 'mocked-Content');

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
            affNoteList: [],
            affNoteInfo: {}
        }
    },
    claimsPrincipal: {
        hasWriteClaim: () => true
    }
};

describe('aff-note - AffNoteContent - connect', () => {
    it('renders without crashing', () => {
        const store = mockStore(initialState);
        const wrapper = shallow(<AffNoteContent store={store} />);
        expect(wrapper).toMatchSnapshot();
    });

    it('should render the container component', () => {
        const store = mockStore(initialState);

        const wrapper = mount(
            <Provider store={store}>
                <AffNoteContent />
            </Provider>
        );

        const container = wrapper.find(AffNoteContent);
        const content = container.find(Content);

        expect(content.exists()).toBe(true);
        expect(content.props().affId).toBe(1);
        expect(content.props().pagingOption).toBe(initialState.note.affNote.pagingOption);
        expect(content.props().noteList).toBe(initialState.note.affNote.affNoteList);
        expect(content.props().noteInfo).toBe(initialState.note.affNote.affNoteInfo);
        expect(content.props().isEditNote).toBe(true);
    });
});
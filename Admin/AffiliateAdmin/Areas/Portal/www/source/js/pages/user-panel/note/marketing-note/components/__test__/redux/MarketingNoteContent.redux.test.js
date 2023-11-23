import React from 'react';
import { mount, shallow } from 'enzyme';
import thunk from 'redux-thunk';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';
import MarketingNoteContent from '../../MarketingNoteContent';
import Content from '../../../../common/components/Content';

jest.mock('../../../../common/components/Content', () => 'mocked-Content');

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
            marketingNoteList: [],
            marketingNoteInfo: {}
        }
    },
    claimsPrincipal: {
        hasWriteClaim: () => false
    }
};

describe('marketing-note - MarketingNoteContent - connect', () => {
    it('renders without crashing', () => {
        const store = mockStore(initialState);
        const wrapper = shallow(<MarketingNoteContent store={store} />);
        expect(wrapper).toMatchSnapshot();
    });

    it('should render the container component', () => {
        const store = mockStore(initialState);

        const wrapper = mount(
            <Provider store={store}>
                <MarketingNoteContent />
            </Provider>
        );

        const container = wrapper.find(MarketingNoteContent);
        const content = container.find(Content);

        expect(content.exists()).toBe(true);
        expect(content.props().affId).toBe(1);
        expect(content.props().pagingOption).toBe(initialState.note.marketingNote.pagingOption);
        expect(content.props().noteList).toBe(initialState.note.marketingNote.marketingNoteList);
        expect(content.props().noteInfo).toBe(initialState.note.marketingNote.marketingNoteInfo);
        expect(content.props().isEditNote).toBe(false);
    });
});
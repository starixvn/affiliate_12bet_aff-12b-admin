import React from 'react';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', ()=> ({render: jest.fn()}))

describe('offer aff - App', () => {
    it('Should render correctly', () => {
        window.globalVariables = {};
        window.initialState = { offerPageList: [], previewLink: '', offerFile: '' };

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})
import React from 'react';
import ReactDOM from 'react-dom';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', ()=> ({render: jest.fn()}))

describe('affiliat info - App', () => {
    it('Should render correctly', () => {
        window.initialState = {
            commGroupList: [
                {
                    Id: "133",
                    Name: "NewGroup"
                },
                {
                    Id: "167",
                    Name: "TEST01"
                },
                {
                    Id: "155",
                    Name: "CG22126"
                }
            ],
            languageList: [
                {
                    LanguageCode: "en-GB",
                    LanguageName: "English"
                },
                {
                    LanguageCode: "vi-VN",
                    LanguageName: "Vietnamese"
                },
                {
                    LanguageCode: "zh-TW",
                    LanguageName: "Chinese Traditional"
                }
            ],
            groupId: 0
        };

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})
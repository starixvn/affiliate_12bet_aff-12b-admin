import React from 'react';
import ReactDOM from 'react-dom';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', ()=> ({render: jest.fn()}))

describe('affiliate report - App', () => {
    beforeEach(() => {
        window.initialState = {
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
            poductList: [
                {
                    ProductId: 1,
                    ProductName: "Sportsbook"
                },
                {
                    ProductId: 2,
                    ProductName: "Playtech Casino"
                },
                {
                    ProductId: 2,
                    ProductName: "Number Game"
                }
            ],
            today: "06/27/2019"
        }
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})
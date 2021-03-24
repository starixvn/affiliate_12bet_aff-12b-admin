import React from 'react';
import { InsertUpdateForm } from '../InsertUpdateForm';
import { shallow } from 'enzyme';
import InputFile from '../../../../common/components/InputFile';
import NotificationPopup from '../../../../common/components/NotificationPopup';

describe('news letter list - components - InsertUpdateForm ', () => {
    const bannerInfo = {
        BannerId: 2485,
        Categories: ["Casino"],
        SubCategories: ["Baccarat"],
        Currencies: ["EURO"],
        DeviceTypes: ["1"],
        AffTypes: ["2"],
        FileName: "CategoryMedia/giphy.gif",
        ImageType: "Image",
        Language: "Chinese Simplified",
        Size: "500x500"
    };

    const param = {
        pageIndex: 1,
        pageSize: 20
    };

    const languageList = [
        { LanguageCode: "en-US", LanguageName: "English" },
        { LanguageCode: "vi-VN", LanguageName: "Vietnamese" }
    ]

    const defaultProps = {
        selectedNewsLetterPageId: 0,
        param,
        languageList
    };

    it('render correctly with insert', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with update', () => {
        const props = Object.assign({}, defaultProps, { selectedNewsLetterPageId: 2485, getNewsLetterPageDetail: jest.fn() });
        const result = shallow(<InsertUpdateForm {...props} />);
        result.setState({
            isInsert: false, messageNotificationPopup: 'abc'
        });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with not show popup', () => {
        const props = Object.assign({}, defaultProps, { selectedNewsLetterPageId: null, languageList: [{ LanguageCode: "vi-VN", LanguageName: "Vietnamese" }] });
        const result = shallow(<InsertUpdateForm {...props} />);
        result.setState({ category: "" })

        expect(result).toMatchSnapshot();
    });
})
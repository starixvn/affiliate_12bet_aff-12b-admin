import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';

describe('news letter - components - Content ', () => {
    const gridData = [
        {
            FileName: "test.html",
            FileSize: 475,
            FromDate: "2021-04-01T00:00:00",
            Language: "hi-IN",
            LanguageName: "English (India)",
            NewsLetterId: 25,
            ToDate: "2021-04-30T00:00:00",
            UploadDate: "2021-03-15T00:43:51.243"
        },
        {
            FileName: "test2.html",
            FileSize: 17722,
            FromDate: "2021-02-28T13:00:00",
            Language: "en-US",
            LanguageName: "English",
            NewsLetterId: 23,
            ToDate: "2021-04-29T13:00:00",
            UploadDate: "2021-03-05T04:03:36.013"
        },
        {
            FileName: "test1.html",
            FileSize: 111,
            FromDate: "2021-03-30T02:00:00",
            Language: "hi-IN",
            LanguageName: "English (India)",
            NewsLetterId: 22,
            ToDate: "2021-04-28T00:00:01",
            UploadDate: "2021-03-04T23:14:51.38"
        }
    ];

    const defaultProps = {
        checkedNewsLetterPages: { 25: false, 23: false, 22: true },
        param: { pageIndex: 1, pageSize: 20 },
        pagingOption: { PageIndex: 1, PageSize: 20, Total: 0 },
        previewLink: 'https://ha.nexdev.net:7613/12BetUserAsianNew/',
        shouldResetScrollPosition: true,
        isModifiable: true,
        gridData
    };

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is null', () => {
        const props = Object.assign({}, defaultProps, { gridData: null })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });
})
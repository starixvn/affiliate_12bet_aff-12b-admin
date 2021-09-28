import React from 'react';
import { Content } from '../Content';
import { shallow, mount } from 'enzyme';
import { Table } from '@9softstudio/react-fixed-table-header';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('registerd member list - components - Content ', () => {

    const gridData = [
        {
            AffName: "12BETAFF017",
            CreatedDate: "2017-07-06T10:24:00",
            CreatedDateText: "07/06/2017 10:24:00",
            FirstName: "",
            LastName: "",
            Currency: "UUS",
            MemberId: 26522982,
            UserName: "12Bet$wjpwjp"
        },
        {
            AffName: "12BETAFF177",
            CreatedDate: "2017-07-05T18:25:00",
            CreatedDateText: "07/05/2017 18:25:00",
            FirstName: "",
            LastName: "",
            Currency: "UUS",
            MemberId: 26518302,
            UserName: "12Bet$jingguoan"
        }
    ];

    const param = {
        AffName: "",
        UserName: "",
        PageIndex: 1,
        PageSize: 20,
    };

    const defaultProps = {
        pagingOption: defaultPagingOption,
        isShowMemberName: true,
        param,
        gridData,
        fetchData: jest.fn(),
        toggleLoader: jest.fn()
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

    it('render correctly with gridData is empty and not isShowMemberName', () => {
        const props = Object.assign({}, defaultProps, { gridData: [], isShowMemberName: false })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('_handlePaging - should be called when onPaging in Table', () => {
        const props = Object.assign({}, defaultProps, { setParam: jest.fn() })
        const result = shallow(<Content {...props} />);

        result.find(Table).props().onPaging(50, 50);

        expect(props.setParam).toHaveBeenCalledWith({ PageIndex: 50, PageSize: 50, AffName: '', UserName: '' });
    });

    it('useEffect - should call fetchData', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() })
        mount(<Content {...props} />);

        expect(props.fetchData).toHaveBeenCalledWith(param);
    });
})
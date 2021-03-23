import React from 'react';
import RowDetail from '../RowDetail';
import { shallow } from 'enzyme';

describe('new view log - components - RowDetail', () => {
    it('render correctly', () => {
        const defaultProps = {
            oldData: "{\"root\":{\"row\":{\"@ContactApps\":\"[{\\\"Name\\\":\\\"MSN/YM/QQ\\\",\\\"Value\\\":\\\"123 Lê Văn\\\"}]\",\"@PageName\":\"11111\",\"@PageCode\":\"1111\"}}}",
            newData: "{\"root\":{\"row\":{\"@PageName\":\"1122111\",\"@PageCode\":\"1111\"}}}",
        }

        const result = shallow(<RowDetail {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly 1', () => {
        const defaultProps = {
            oldData: "{\"root\":{\"row\":{\"@ContactApps\":\"[{\\\"Name\\\":\\\"MSN/YM/QQ\\\",\\\"Value\\\":\\\"123 Lê Văn\\\"}]\",\"@PageName\":\"11111\",\"@PageCode\":\"1111\", \"@Name\":\"\"}}}",
            newData: "",
        }

        const result = shallow(<RowDetail {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly 2', () => {
        const defaultProps = {
            oldData: "{\"root\":{\"row\":null}}",
            newData: "{\"root\":{\"row\":{\"@PageName\":\"1122111\"}}}",
        }

        const result = shallow(<RowDetail {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });
});
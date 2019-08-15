import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Form, FormGroup, Input, Label } from 'reactstrap';

import { selectedAllProduct, globalResource } from '../modules/constants';
import { setParam, getProductList } from '../actions';
import { htmlDecode } from '../../../../modules/htmlHelper';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { param } = this.props;

        this.state = {
            productList: [],
            selectedProduct: param.productId || selectedAllProduct
        }
    }

    componentDidMount() {
        this._fetchProductList();
        this.handleSetParam(this.state.selectedProduct);
    }

    async _fetchProductList() {
        const productList = await this.props.getProductList();
        this.setState({ productList });
    }

    handleChangeProduct = (event) => {
        const selectedProduct = Number(event.target.value);

        this.setState({ selectedProduct });
        this.handleSetParam(selectedProduct);
    }

    handleSetParam(selectedProduct) {
        const param = Object.assign(this.props.param, { productId: selectedProduct });
        this.props.setParam(param);
    }

    render() {
        const { productList, selectedProduct } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-2">
                        <Label className="mr-2">Search By</Label>
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">{globalResource.All}</option>
                            {
                                productList.map(p => {
                                    return (<option key={`product-${p.ProductId}`} value={p.ProductId}>{htmlDecode(p.ProductName)}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ dailyEarningDetail }) {
    const { param } = dailyEarningDetail;
    return Object.assign({}, { param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, getProductList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);
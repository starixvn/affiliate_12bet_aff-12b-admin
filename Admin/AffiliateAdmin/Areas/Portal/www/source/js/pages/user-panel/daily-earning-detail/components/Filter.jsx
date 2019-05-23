import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Form, FormGroup, Input, Label } from 'reactstrap';
import { globalResource } from '../../../../modules/globalResource';
import { setParam } from '../actions';
import { getProductList } from '../../../customer/user-panel/modules/service';
import { selectedAllProduct } from '../../../../modules/constants';

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
        this.props.setParam({...this.props.param, productId: this.state.selectedProduct});
    }

    async _fetchProductList() {
        const productList = await getProductList();
        this.setState({ productList });
    }

    handleChangeProduct = (event) => {
        const selectedProduct = Number(event.target.value);
        const param = { ...this.props.param, productId: selectedProduct };

        this.props.setParam(param);
        this.setState({ selectedProduct });
    }

    render() {
        const { productList, selectedProduct } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-2">Search By</Label>
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">{globalResource.All}</option>
                            {
                                productList.map(p => {
                                    return (<option value={p.ProductId}>{p.ProductName}</option>)
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
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);
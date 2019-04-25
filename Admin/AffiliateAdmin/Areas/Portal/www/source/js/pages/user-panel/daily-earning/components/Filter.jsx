import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Form, FormGroup, Input, Label } from 'reactstrap';

import { setParam } from '../actions';
import { selectedAllProduct } from '../../../../modules/constants';
import { getProductList } from '../../../customer/user-panel/modules/service';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            selectedProduct: selectedAllProduct,
            productList: []
        }
    }

    componentDidMount() {
        if (this.state.productList.length === 0) {
            this._fetchProductList();
        }
    }

    async _fetchProductList(){
        const productList = await getProductList();
        this.setState({ productList });
    }

    handleChangeProduct = (event) => {
        const selectedProduct = Number(event.target.value);
        this.setState({ selectedProduct });
        this._submit(selectedProduct);
    }

    _submit(selectedProduct) {
        const param = Object.assign(this.props.param, { productId: selectedProduct });
        this.props.setParam(param);
    }

    render() {
        const { productList, selectedProduct } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-2">Search By</Label>
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">All</option>
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

function mapStateToProps({ userPanel }) {
    const { param } = userPanel;
    return Object.assign({}, { param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);
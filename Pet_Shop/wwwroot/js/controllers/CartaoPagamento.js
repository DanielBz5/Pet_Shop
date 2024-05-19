export default class MercadoPagoController {
    constructor() {
        this.mp = null;
        this.cardForm = null;

        this.selectCard = this.selectCard.bind(this);
    }

    async initializeMercadoPago() {
        return new Promise((resolve, reject) => {
            // Crie um elemento de script
            const script = document.createElement('script');
            // Defina o atributo src para o URL do SDK do Mercado Pago
            script.src = 'https://sdk.mercadopago.com/js/v2';
            // Defina o evento de carregamento do script
            script.onload = () => {
                // Após o carregamento do SDK, inicialize o Mercado Pago
                this.mp = new window.MercadoPago("TEST-236a3f59-a842-4fcd-84ca-3245c93ab042");
                // Resolva a Promise para indicar que o Mercado Pago foi inicializado com sucesso
                resolve();
            };
            // Defina o evento de erro do script
            script.onerror = reject;
            // Adicione o elemento de script ao corpo do documento
            document.body.appendChild(script);
        });
    }

    createCardForm() {
        this.cardForm = this.mp.cardForm({
            amount: "10.0",
            iframe: true,
            form: {
                id: "form-checkout",
                cardNumber: {
                    id: "form-checkout__cardNumber",
                    placeholder: "Número do cartão",
                },
                expirationDate: {
                    id: "form-checkout__expirationDate",
                    placeholder: "MM/YY",
                },
                securityCode: {
                    id: "form-checkout__securityCode",
                    placeholder: "Código de segurança",
                },
                cardholderName: {
                    id: "form-checkout__cardholderName",
                    placeholder: "Titular do cartão",
                },
                issuer: {
                    id: "form-checkout__issuer",
                    placeholder: "Banco emissor",
                },
                installments: {
                    id: "form-checkout__installments",
                    placeholder: "Parcelas",
                },
                identificationType: {
                    id: "form-checkout__identificationType",
                    placeholder: "Tipo de documento",
                },
                identificationNumber: {
                    id: "form-checkout__identificationNumber",
                    placeholder: "Número do documento",
                },
                cardholderEmail: {
                    id: "form-checkout__cardholderEmail",
                    placeholder: "E-mail",
                },
            },
            callbacks: {
                onFormMounted: error => {
                    if (error) {
                        this.MessageBox('Atenção', error);
                    } 
                },
            },
        });
    }

    Submit() {
        event.preventDefault();  //Fez o Post json null      Cartão teste :5031 4332 1540 6351/ Data: 11/25  CS:123

        const {
            paymentMethodId: payment_method_id,
            issuerId: issuer_id,
            cardholderEmail: email,
            amount,
            token,
            installments,
            identificationNumber,
            identificationType,
        } = this.cardForm.getCardFormData();

        fetch('ProcessCard', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                token,
                issuer_id,
                payment_method_id,
                transaction_amount: Number(amount),
                installments: Number(installments),
                description: "Descrição do produto",
                payer: {
                    email,
                    identification: {
                        type: identificationType,
                        number: identificationNumber,
                    },
                },
            }),
        })
            .then(response => {
                if (response.ok) {
                    this.fechaView();
                    document.getElementById('continua-pedido').style.display = 'block';
                    document.getElementById('mostra-cartao').style.display = 'block'
                } else {
                    this.MessageBox('Atenção', 'Erro ao Validar Cartão.');
                }
            })
    }

    selectCard() {
        const metodo = document.getElementById('select-pagamento').value;
        if (metodo == "Cartao") {
            const viewCartao = document.getElementById('cartao-view');
            viewCartao.style.display = 'flex';

            const overlay = document.getElementById('overlay');
            overlay.style.display = 'block';

            const btnContinua = document.getElementById('continua-pedido');
            btnContinua.style.display = 'none';
            

            this.initializeMercadoPago().then(() => {
                this.createCardForm();
            }).catch(error => {
                this.MessageBox('Atenção','Erro ao Carregar Adiciona Cartão')
            });
            
        }
        else {
            documento.getElementById('cartao-view').style.display = 'none'
        }
    }

    fechaView() {
        const viewCartao = document.getElementById('cartao-view');
        viewCartao.style.display = 'none';

        const overlay = document.getElementById('overlay');
        overlay.style.display = 'none';
    }

    MessageBox(titulo, mensagem) {
        const url = `MessageBox?titulo=${encodeURIComponent(titulo)}&mensagem=${encodeURIComponent(mensagem)}`;
        window.location.href = url;
    }

}
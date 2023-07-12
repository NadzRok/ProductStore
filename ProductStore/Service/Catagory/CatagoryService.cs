namespace ProductStore.Service.Catagory {
    public class CatagoryService : ICatagoryService {
        public bool CheckCatagoryCode(string CatagoryCode) {
            if (CatagoryCode.Length != 6 ) {
                return false;
            }
            for (int i = 0; i < CatagoryCode.Length; i++) {
                if(i < 3) {
                    if(!char.IsLetter(CatagoryCode[i])) {
                        return false;
                    }
                } else if(i > 2) {
                    if(!char.IsNumber(CatagoryCode[i])) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 아키텍처: 설계 그 잡채(설계마다 철학이 있다.)
// 디자인 패턴: 설계를 구현하는 과정에서 쓰이는 패턴

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;
    
    // 도메인에 변화가 있을 때 호출되는 액션
    public event Action OnDataChanged;

    private CurrencyRepository _repository;
    
    // 마틴 아저씨: 미리하는 성능 최적화의 90%는 의미가 없다.
    // public event Action OnGoldChanged;
    // public event Action OnDiamondChanged;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        // 생성
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);

        // 레포지토리(깃허브)
        _repository = new CurrencyRepository();

        // 이후 열거형이 추가되더라도 알아서 잘 삽입되게 변경 완료
        List<CurrencyDTO> loadedCurrencies = _repository.Load();
        for (int i = 0; i < (int)ECurrencyType.Count; ++i)
        {
            ECurrencyType type = (ECurrencyType)i;
            CurrencyDTO loadedCurrency = loadedCurrencies?.Find(data => data.Type == type);

            // 저장된 데이터가 있다면 그 값.. 없다면 0
            Currency currency = new Currency(type, loadedCurrency != null ? loadedCurrency.Value : 0);
            
            _currencies.Add(type, currency);
        }
    }

    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);
        
        // 다양한 이유로 여기에 규칙이 들어가기도한다.

        _repository.Save(ToDtoList());
        
        OnDataChanged?.Invoke();
    }
    
    
    public bool TryBuy(ECurrencyType type, int value)
    {
        if (!_currencies[type].TryBuy(value))
        {
            return false;
        }
        
        _repository.Save(ToDtoList());

        OnDataChanged?.Invoke();

        return true;
    }
    
}
